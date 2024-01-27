using Domain.Customers.Model.CustomerAggregate;
using Domain.Orders.OrderAggregate;
using Domain.Products.ProductAggregate;

namespace Application.UseCases.Orders
{
    public interface ICreateOrderUseCase : IUseCase<CreateOrderRequest, CreateOrderResponse>;
    public sealed class CreateOrderUseCase : ICreateOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<CreateOrderResponse> Execute(CreateOrderRequest request)
        {
            try {
                var order = new Order(request.id, request.orderItems, request.customer, new OrderStatus(EOrderStatus.PaymentPending), DateTime.Now);
                _orderRepository.Add(order);

                var returnResponse = new CreateOrderResponse(order.Customer.Name, order.Status.ToString(), order.Id);

                return returnResponse;

            }catch (Exception ex)
            {

            }

            throw new NotImplementedException();
        }
    }


    public record CreateOrderRequest(Guid id, IList<OrderItem> orderItems, Customer customer, OrderStatus status, DateTime createAt);

    public record CreateOrderResponse(string CostumerName, string StatusOrder, Guid orderId );
}
