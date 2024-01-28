using Application.UseCases.Orders.Validators;
using Domain.Customers.Model.CustomerAggregate;
using Domain.Orders.OrderAggregate;
using Domain.Products.ProductAggregate;
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
            // TODO: Improve validations
            await _validator.Validate(request);

            var customer = GetCustomer(request.CustomerId);
            var order = new Order(customer);

            foreach (var item in request.OrderItems)
            {
                var product = GetProduct(item.ProductId);
                order.AddOrderItem(new OrderItem(product, item.Quantity));
            }

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

        return customerRepository.GetById(customerId.Value); // TODO: Is it an existing product?
    }

    private Product? GetProduct(Guid productId)
    {
        return productRepository.GetById(productId); // TODO: Is it an existing product?
    }
}

public record CreateOrderRequest(IEnumerable<CreateOrderItemRequest> OrderItems, Guid? CustomerId = null);

public record CreateOrderItemRequest(Guid ProductId, short Quantity);

public record CreateOrderResponse(Guid OrderId, string StatusOrder);