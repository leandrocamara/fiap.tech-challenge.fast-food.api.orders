using Domain.Orders.Model.OrderAggregate;
using Domain.SeedWork;

namespace Application.UseCases.Orders;

public interface IGetOngoingOrdersUseCase : IUseCase<IEnumerable<OrderResponse>>;

public sealed class GetOngoingOrdersUseCase(IOrderRepository orderRepository) : IGetOngoingOrdersUseCase
{
    public async Task<IEnumerable<OrderResponse>> Execute()
    {
        try
        {
            var orders = await orderRepository.GetOngoingOrders();

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
}