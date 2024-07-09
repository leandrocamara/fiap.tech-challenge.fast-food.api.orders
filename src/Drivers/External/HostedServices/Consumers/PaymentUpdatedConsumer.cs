using Adapters.Controllers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace External.HostedServices.Consumers;

public sealed class PaymentUpdatedConsumer(
    ILogger<PaymentUpdatedConsumer> logger,
    IOrderController orderController) : IConsumer<PaymentUpdated>
{
    public const string QueueName = "payment-updated";

    public async Task Consume(ConsumeContext<PaymentUpdated> context)
    {
        var paymentUpdated = context.Message;
        logger.LogInformation("Received message: {Text}", JsonConvert.SerializeObject(paymentUpdated));

        // await orderController.UpdateOrderStatus(paymentReturn.OrderId, paymentReturn.Paid);
    }
}

public record PaymentUpdated(Guid OrderId, bool Paid); // TODO: Move to UseCase