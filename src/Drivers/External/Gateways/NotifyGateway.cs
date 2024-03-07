using Application.Gateways;
using Entities.Orders.OrderAggregate;

namespace External.Gateways;

internal class NotifyGateway : INotifyGateway
{
    public void NotifyOrderPaymentUpdate(Order order)
    {
        //throw new NotImplementedException();
    }

    public void NotifyOrderStatusUpdate(Order order)
    {
        //throw new NotImplementedException();
    }
}