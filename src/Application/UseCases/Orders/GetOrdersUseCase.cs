using Domain.Orders.Model.OrderAggregate;
using Domain.SeedWork;

namespace Application.UseCases.Orders
{
    public interface IGetOrdersUseCase : IUseCase<GetOrderRequest, IEnumerable<GetOrderResponse>>;


    public sealed class GetOrdersUseCase(IOrderRepository orderRepository) : IGetOrdersUseCase
    {
        public async Task<IEnumerable<GetOrderResponse>> Execute(GetOrderRequest request)
        {
            try
            {
                var orders = await orderRepository.GetOrders();

                if (!orders.Any())
                    throw new ApplicationException("Orders not found");

                return orders.Select(order => new GetOrderResponse(
                   order.Id,
                   order.OrderNumber,
                   order.Status.ToString()));
            }
            catch (DomainException e)
            {
                throw new ApplicationException($"Failed to recover orders. Error: {e.Message}", e);
            }
        }
    }


    public record GetOrderRequest();

    public record GetOrderResponse(Guid Id, int OrderNumber, string status);
}