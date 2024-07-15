using Entities.Orders.OrderAggregate;

namespace Application.Gateways;

public interface IPaymentGateway
{
    Task<string> GenerateQrCode(Order order);
}