using Entities.Orders.OrderAggregate;

namespace Adapters.Gateways.Notifications;

public interface IOrderStatusNotificationClient
{
    Task Notify(OrderStatusNotification notification);
}

public record OrderStatusNotification(int OrderNumber, string Status, DateTime NotifiedAt);