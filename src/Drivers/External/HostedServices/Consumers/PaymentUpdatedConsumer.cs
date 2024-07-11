using Adapters.Controllers;
using Amazon.SQS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace External.HostedServices.Consumers;

public sealed class PaymentUpdatedConsumer(
    IServiceProvider serviceProvider,
    IAmazonSQS sqsClient,
    ILogger<SqsConsumerHostedService<PaymentUpdated>> logger)
    : SqsConsumerHostedService<PaymentUpdated>(serviceProvider, sqsClient, logger)
{
    protected override string QueueName() => "payment-updated";

    protected override async Task Process(IServiceScope scope, PaymentUpdated paymentUpdated)
    {
        var orderController = scope.ServiceProvider.GetRequiredService<IOrderController>();
        await orderController.UpdatePaymentStatus(paymentUpdated.OrderId, paymentUpdated.Paid);
    }
}

public record PaymentUpdated(Guid OrderId, bool Paid);