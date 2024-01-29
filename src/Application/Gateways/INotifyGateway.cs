using Domain.Orders.Model.OrderAggregate;

namespace Application.Gateways;

public interface INotifyGateway
{
    void NotifyOrderPaymentUpdate(Order order);
    void NotifyOrderStatusUpdate(Order order);
}