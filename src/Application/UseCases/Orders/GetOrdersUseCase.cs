using Domain.Orders.Model.OrderAggregate;
using Domain.SeedWork;

namespace Application.UseCases.Orders;

public interface IGetOrdersUseCase : IUseCase<IEnumerable<OrderResponse>>;

public sealed class GetOrdersUseCase(IOrderRepository orderRepository) : IGetOrdersUseCase
{
    public async Task<IEnumerable<OrderResponse>> Execute()
    {
        try
        {
            var orders = await orderRepository.GetOrders();

            if (!orders.Any())
                throw new ApplicationException("Orders not found");

            return orders.Select(order => new OrderResponse(order));
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover orders. Error: {e.Message}", e);
        }
    }
}

public record OrderResponse(
    Guid Id,
    string? Customer,
    string Status,
    decimal TotalPrice)
{
    public OrderResponse(Order order) : this(
        order.Id,
        order.Customer?.Name,
        order.Status,
        order.TotalPrice)
    {
    }

    public record GetOrderRequest();

    public record GetOrderResponse(Guid Id, int OrderNumber, string status);
}