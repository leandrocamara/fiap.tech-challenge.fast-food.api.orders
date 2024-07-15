using Adapters.Gateways.Tickets;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace External.Clients.Tickets;

public class TicketClient(IAmazonSQS sqsClient, ILogger<TicketClient> logger) : ITicketClient
{
    private const string QueueName = "ticket-created";

    public Task SendTicket(Ticket ticket)
    {
        var message = JsonConvert.SerializeObject(ticket);
        logger.LogInformation("Publishing message: {Text}", message);

        return sqsClient.SendMessageAsync(new SendMessageRequest { QueueUrl = QueueName, MessageBody = message });
    }
}