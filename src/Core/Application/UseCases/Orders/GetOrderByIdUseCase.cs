using Application.Gateways;
using Entities.Orders.OrderAggregate;
using Entities.SeedWork;

namespace Application.UseCases.Orders
{
    public interface IGetOrderByIdUseCase : IUseCase<Guid, GetOrderByIdResponse>;

    public sealed class GetOrderByIdUseCase(IOrderGateway orderGateway) : IGetOrderByIdUseCase
    {
        public Task<GetOrderByIdResponse> Execute(Guid id)
        {
            try
            {
                var order = orderGateway.GetById(id);

                if (order == null)
                    throw new ApplicationException("Order not found");

                return Task.FromResult(new GetOrderByIdResponse(order));
            }
            catch (DomainException e)
            {
                throw new ApplicationException($"Failed to recover product. Error: {e.Message}", e);
            }
        }
    }

    public record GetOrderByIdResponse(
        Guid Id,
        int Number,
        Guid? CustomerId,
        string? Customer,
        string Status,
        decimal TotalPrice,
        IEnumerable<OrderItemResponse> Items)
    {
        public GetOrderByIdResponse(Order order) : this(
            order.Id,
            order.OrderNumber,
            order.CustomerId,
            order.Customer?.Name,
            order.Status,
            order.TotalPrice,
            order.OrderItems.Select(item => new OrderItemResponse(item)))
        {
        }
    }

    public record OrderItemResponse(Guid Id, string Product, int Quantity, decimal TotalPrice)
    {
        public OrderItemResponse(OrderItem item) : this(item.Id, item.Product.Name, item.Quantity, item.TotalPrice)
        {
        }
    }
}
