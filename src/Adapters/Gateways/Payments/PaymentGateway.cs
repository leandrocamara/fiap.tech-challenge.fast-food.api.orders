using Application.Gateways;
using Entities.Orders.OrderAggregate;

namespace Adapters.Gateways.Payments;

public class PaymentGateway(IPaymentClient client) : IPaymentGateway
{
    public async Task<string> GenerateQrCode(Order order)
    {
        var payment = await client.CreatePayment(order.Id, order.TotalPrice);
        return payment.QrCode;
    }
}