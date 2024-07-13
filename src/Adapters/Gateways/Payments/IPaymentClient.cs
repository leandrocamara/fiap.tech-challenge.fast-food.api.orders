namespace Adapters.Gateways.Payments;

public interface IPaymentClient
{
    Task<Payment> CreatePayment(Guid orderId, decimal amount);
}

public record Payment(string QrCode);