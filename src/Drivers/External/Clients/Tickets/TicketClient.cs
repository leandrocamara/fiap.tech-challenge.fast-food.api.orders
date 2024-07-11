using Adapters.Gateways.Tickets;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace External.Clients.Tickets;

public class TicketClient(IAmazonSQS sqsClient, ILogger<TicketClient> logger) : ITicketClient
{
    private const string QueueUrl = "ticket-created"; // TODO: From env variable

    public async Task SendTicket(Ticket ticket)
    {
        logger.LogInformation("Publishing message: {Text}", JsonConvert.SerializeObject(ticket));

        await sqsClient.SendMessageAsync(new SendMessageRequest
        {
            QueueUrl = QueueUrl,
            MessageBody = JsonConvert.SerializeObject(ticket) // TODO: Define contract
        });
    }
}