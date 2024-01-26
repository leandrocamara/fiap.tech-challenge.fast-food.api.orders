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
        public Task<CreateOrderResponse> Execute(CreateOrderRequest request)
        {
            try {
                var order = new Order(request.products, request.customer, Guid.NewGuid());
                _orderRepository.Add(order);

              
                return new CreateOrderResponse(                    
                    order.Customer.Name,
                    order.status.ToString());

            }catch (Exception ex)
            {

            }

            throw new NotImplementedException();
        }
    }


    public record CreateOrderRequest(int statusOrder,List<Product> products, Customer customer);

    public record CreateOrderResponse(string CostumerName, string StatusOrder );
}
