using Application.UseCases.Customers;
using Domain.Customer.Model.CustomerAggregate;
using Domain.Order.OrderAggregate;
using Domain.Products.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    public record CreateOrderRequest(int statusOrder,List<Product> products, Domain.Customer.Model.CustomerAggregate.Customer customer);

    public record CreateOrderResponse(string CostumerName, string StatusOrder );
}
