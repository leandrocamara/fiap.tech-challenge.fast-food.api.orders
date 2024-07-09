using Adapters.Gateways.Tickets;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace External.Clients.Tickets;

public class TicketClient(IPublishEndpoint publishEndpoint, ILogger<TicketClient> logger) : ITicketClient
{
    public Task SendTicket(Ticket ticket)
    {
        logger.LogInformation("Publishing first message: {Text}", JsonConvert.SerializeObject(ticket));
        return publishEndpoint.Publish(ticket);
    }
}