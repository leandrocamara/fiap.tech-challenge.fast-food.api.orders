using Adapters.Gateways.Notifications;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace External.Clients.Notifications.OrderStatus;

internal class OrderStatusNotificationClient(
    IAmazonSQS sqsClient,
    ILogger<OrderStatusNotificationClient> logger) : IOrderStatusNotificationClient
{
    private const string QueueName = "order-status-updated";

    public Task Notify(OrderStatusNotification notification)
    {
        var message = JsonConvert.SerializeObject(notification);
        logger.LogInformation("Publishing message: {Text}", message);

        return sqsClient.SendMessageAsync(new SendMessageRequest { QueueUrl = QueueName, MessageBody = message });
    }
}