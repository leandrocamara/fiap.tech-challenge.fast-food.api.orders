using Domain.Customers.Model.CustomerAggregate;
using Domain.Orders.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Orders
{
    public interface IGetOrderUseCase : IUseCase<CreateOrderRequest, IEnumerable<Order>>;


    public sealed class GetOrdersUseCase : IGetOrderUseCase
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


