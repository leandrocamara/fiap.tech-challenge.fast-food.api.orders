using Adapters.Gateways.Payments;
using Microsoft.Extensions.Options;

namespace External.Clients.Payments;

public class PaymentClient(IOptions<PaymentsClientSettings> paymentsClientSettings) : IPaymentClient
{
    public Task<Payment> CreatePayment(Guid orderId, decimal amount)
    {
        // TODO: HTTP sync
        throw new NotImplementedException();
    }
}