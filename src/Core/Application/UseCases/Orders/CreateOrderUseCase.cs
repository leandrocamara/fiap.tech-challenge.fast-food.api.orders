using Application.ACL.Payment;
using Application.UseCases.Orders.Validators;
using Entities.Customers.CustomerAggregate;
using Entities.Orders.OrderAggregate;
using Entities.Products.ProductAggregate;
using Entities.SeedWork;
using QRCoder;

namespace Application.UseCases.Orders;

public interface ICreateOrderUseCase : IUseCase<CreateOrderRequest, CreateOrderResponse>;

public sealed class CreateOrderUseCase(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IProductRepository productRepository,
    IPaymentGateway paymentGateway) : ICreateOrderUseCase
{
    private readonly OrderCreationValidator _validator = new(customerRepository, productRepository);

    private static string GetBase64QrCodeImage(string qrCodText)
    {
        QRCodeGenerator qrGenerator = new();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodText, QRCodeGenerator.ECCLevel.Q);
        PngByteQRCode qrCode = new(qrCodeData);
        return Convert.ToBase64String(qrCode.GetGraphic(20));
    }

    public async Task<CreateOrderResponse> Execute(CreateOrderRequest request)
    {
        try
        {
            await _validator.Validate(request);

            var orderItems = new List<OrderItem>();

            foreach (var item in request.OrderItems)
            {
                var product = productRepository.GetById(item.ProductId);
                orderItems.Add(new OrderItem(product, item.Quantity));
            }

            var customer = GetCustomer(request.CustomerId);
            var order = new Order(customer, orderItems, orderRepository.GetNextOrderNumber());
            order.SetQrCode(paymentGateway.GetQrCodeForPay(order));
            orderRepository.Add(order);

            return new CreateOrderResponse(order.Id, order.OrderNumber, order.Status.ToString(), GetBase64QrCodeImage(order.QrCodePayment));
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to register the order. Error: {e.Message}", e);
        }
    }

    private Customer? GetCustomer(Guid? customerId)
    {
        if (customerId is null)
            return null;

        return customerRepository.GetById(customerId.Value);
    }
}

public record CreateOrderRequest(IEnumerable<OrderItemRequest> OrderItems, Guid? CustomerId = null);
public record OrderItemRequest(Guid ProductId, short Quantity);

public record CreateOrderResponse(Guid Id, int Number, string Status, string? Base64PngQrCode);
