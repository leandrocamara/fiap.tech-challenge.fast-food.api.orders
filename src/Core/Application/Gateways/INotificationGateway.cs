using Entities.Orders.OrderAggregate;

namespace Application.Gateways;

public interface INotificationGateway
{
    Task NotifyOrderStatusUpdate(Order order);
}