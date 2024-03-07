using Entities.Orders.OrderAggregate;

namespace Application.Gateways;

public interface INotifyGateway
{
    void NotifyOrderPaymentUpdate(Order order);
    void NotifyOrderStatusUpdate(Order order);
}