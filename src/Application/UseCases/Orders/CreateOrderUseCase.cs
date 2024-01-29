using Application.UseCases.Orders.Validators;
using Domain.Customers.Model.CustomerAggregate;
using Domain.Orders.Model.OrderAggregate;
using Domain.Products.Model.ProductAggregate;
using Domain.SeedWork;

namespace Application.UseCases.Orders;

public interface ICreateOrderUseCase : IUseCase<CreateOrderRequest, CreateOrderResponse>;

public sealed class CreateOrderUseCase(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IProductRepository productRepository) : ICreateOrderUseCase
{
    private readonly OrderCreationValidator _validator = new(customerRepository, productRepository);

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
            var order = new Order(customer, orderItems);

            orderRepository.Add(order);

            return new CreateOrderResponse(order.Id, order.Status.ToString());
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

public record CreateOrderResponse(Guid Id, string Status);