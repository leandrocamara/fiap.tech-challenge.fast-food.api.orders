using Domain.Orders.Model.OrderAggregate;
using Domain.SeedWork;

namespace Application.UseCases.Orders;

public interface IGetOrderByIdUseCase : IUseCase<Guid, GetOrderByIdResponse>;

public sealed class GetOrderByIdUseCase(IOrderRepository orderRepository) : IGetOrderByIdUseCase
{
    public async Task<GetOrderByIdResponse> Execute(Guid id)
    {
        try
        {
            var order = orderRepository.GetById(id);

            if (order == null)
                throw new ApplicationException("Order not found");

            return new GetOrderByIdResponse(order);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover product. Error: {e.Message}", e);
        }
    }
}

public record GetOrderByIdResponse(
    Guid Id,
    string? Customer,
    string Status,
    decimal TotalPrice,
    IEnumerable<OrderItemResponse> Items)
{
    public GetOrderByIdResponse(Order order) : this(
        order.Id,
        order.Customer?.Name,
        order.Status,
        order.TotalPrice,
        order.OrderItems.Select(item => new OrderItemResponse(item)))
    {
    }
}

public record OrderItemResponse(Guid Id, string Product, int Quantity)
{
    public OrderItemResponse(OrderItem item) : this(item.Id, item.Product.Name, item.Quantity)
    {
    }
}