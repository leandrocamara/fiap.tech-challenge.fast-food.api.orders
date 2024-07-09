using Adapters.Controllers;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace External.HostedServices.Consumers;

public sealed class TicketUpdatedConsumer(
    ILogger<TicketUpdatedConsumer> logger,
    IOrderController orderController) : IConsumer<TicketUpdated>
{
    public const string QueueName = "ticket-updated";

    public async Task Consume(ConsumeContext<TicketUpdated> context)
    {
        var ticketUpdated = context.Message;
        logger.LogInformation("Received message: {Text}", JsonConvert.SerializeObject(ticketUpdated));

        // await orderController.UpdateOrderStatus(ticketUpdated.OrderId, paymentReturn.Paid); // TODO: Define contract
    }
}

public record TicketUpdated(Guid OrderId, bool Paid); // TODO: Move to UseCase