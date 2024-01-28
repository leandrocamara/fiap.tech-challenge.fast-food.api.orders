using Domain.Orders.Model.OrderAggregate;

namespace Application.UseCases.Orders
{
    public interface IGetOrdersUseCase : IUseCase<CreateOrderRequest, IEnumerable<Order>>;


    public sealed class GetOrdersUseCase : IGetOrdersUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<Order>> Execute(CreateOrderRequest request)
        {
            var resultOrders = await _orderRepository.GetOrders();

            return resultOrders;
        }
    }


    public record GetOrderRequest();

    public record GetOrderResponse();
}