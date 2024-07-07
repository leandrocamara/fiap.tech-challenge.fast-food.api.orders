using Adapters.Gateways.Payments;

namespace External.Clients;

public class PaymentClient : IPaymentClient
{
    public Task<Payment> CreatePayment(Guid orderId, decimal amount)
    {
        // TODO: HTTP sync
        throw new NotImplementedException();
    }
}