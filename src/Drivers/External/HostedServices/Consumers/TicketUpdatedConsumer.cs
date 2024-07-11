using Adapters.Controllers;
using Amazon.SQS;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace External.HostedServices.Consumers;

public sealed class TicketUpdatedConsumer(
    IServiceProvider serviceProvider,
    IAmazonSQS sqsClient,
    ILogger<SqsConsumerHostedService<TicketUpdated>> logger)
    : SqsConsumerHostedService<TicketUpdated>(serviceProvider, sqsClient, logger)
{
    protected override string QueueName() => "ticket-updated";

    protected override Task Process(IServiceScope scope, TicketUpdated ticketUpdated)
    {
        var orderController = scope.ServiceProvider.GetRequiredService<IOrderController>();
        return orderController.UpdateOrderStatus(ticketUpdated.OrderId, ticketUpdated.TicketStatus);
    }
}

public record TicketUpdated(Guid OrderId, string TicketStatus);