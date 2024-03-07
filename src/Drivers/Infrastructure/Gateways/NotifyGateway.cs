using Application.Gateways;
using Domain.Orders.Model.OrderAggregate;

namespace Infrastructure.Gateways;

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