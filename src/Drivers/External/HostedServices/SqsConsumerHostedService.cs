using Amazon.SQS;
using Amazon.SQS.Model;
using External.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace External.HostedServices;

public abstract class SqsConsumerHostedService<T>(
    IServiceProvider serviceProvider,
    IAmazonSQS sqsClient,
    ILogger<SqsConsumerHostedService<T>> logger) : BackgroundService
{
    protected abstract string QueueName();

    protected abstract Task Process(IServiceScope scope, T message);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = QueueName(), MaxNumberOfMessages = 10, WaitTimeSeconds = 5
            };

            try
            {
                var receiveMessageResponse = await sqsClient.ReceiveMessageAsync(receiveMessageRequest, stoppingToken);

                foreach (var message in receiveMessageResponse.Messages)
                {
                    logger.LogInformation($"Message received: {message.Body}");

                    await WrapTransactionalProcess(JsonConvert.DeserializeObject<T>(message.Body)!);

                    var deleteMessageRequest = new DeleteMessageRequest
                    {
                        QueueUrl = QueueName(), ReceiptHandle = message.ReceiptHandle
                    };

                    await sqsClient.DeleteMessageAsync(deleteMessageRequest, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error receiving messages: {ex.Message}");
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    private async Task WrapTransactionalProcess(T message)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<OrdersContext>();
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.BeginTransactionAsync() ??
                                          throw new Exception("Error initializing a transaction");

            try
            {
                await Process(scope, message);
                await dbContext.CommitTransactionAsync(transaction);
            }
            catch (Exception e)
            {
                dbContext.RollbackTransaction();
                throw;
            }
            finally
            {
                await dbContext.DisposeAsync();
            }
        });
    }
}