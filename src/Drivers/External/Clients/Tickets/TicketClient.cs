using Adapters.Gateways.Tickets;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace External.Clients.Tickets;

public class TicketClient(ISendEndpointProvider sendEndpointProvider, ILogger<TicketClient> logger) : ITicketClient
{
    private const string QueueName = "ticket-created";

    public async Task SendTicket(Ticket ticket)
    {
        logger.LogInformation("Publishing message: {Text}", JsonConvert.SerializeObject(ticket));

        var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{QueueName}"));

        await endpoint.Send(ticket); // TODO: Define contract
    }
}