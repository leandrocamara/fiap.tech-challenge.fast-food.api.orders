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

    public Task Consume(ConsumeContext<TicketUpdated> context)
    {
        var ticketUpdated = context.Message;
        logger.LogInformation("Received message: {Text}", JsonConvert.SerializeObject(ticketUpdated));

        return orderController.UpdateOrderStatus(ticketUpdated.OrderId, ticketUpdated.TicketStatus);
    }
}

public record TicketUpdated(Guid OrderId, string TicketStatus);