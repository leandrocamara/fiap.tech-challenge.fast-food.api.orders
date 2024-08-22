using Application.Gateways;
using Entities.Orders.OrderAggregate;

namespace Adapters.Gateways.Notifications;

public class NotificationGateway(IOrderStatusNotificationClient orderStatusNotificationClient) : INotificationGateway
{
    public Task NotifyOrderStatusUpdate(Order order)
    {
        var notification = new OrderStatusNotification(order.OrderNumber, order.Status, DateTime.Now);
        return orderStatusNotificationClient.Notify(notification);
    }
}