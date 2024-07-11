using Adapters.Controllers;
using Amazon.SQS;
using External.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace External.HostedServices.Consumers;

public sealed class PaymentUpdatedConsumer(
    IServiceProvider serviceProvider,
    IAmazonSQS sqsClient,
    ILogger<SqsConsumerHostedService<PaymentUpdated>> logger)
    : SqsConsumerHostedService<PaymentUpdated>(sqsClient, logger)
{
    protected override string QueueName() => "payment-updated";

    protected override async Task Process(PaymentUpdated paymentUpdated)
    {
        using var scope = serviceProvider.CreateScope();
        var orderController = scope.ServiceProvider.GetRequiredService<IOrderController>();

        // TODO: Abstract this
        var dbContext = scope.ServiceProvider.GetRequiredService<FastFoodContext>();
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await dbContext.BeginTransactionAsync() ??
                                          throw new Exception("Error initializing a transaction");

            try
            {
                // TODO: Transform this at param/func
                await orderController.UpdatePaymentStatus(paymentUpdated.OrderId, paymentUpdated.Paid);
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

public record PaymentUpdated(Guid OrderId, bool Paid);