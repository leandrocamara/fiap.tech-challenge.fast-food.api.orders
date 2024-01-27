using Domain.Customers.Model.CustomerAggregate;
using Domain.Orders.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Orders
{
    public interface IGetOrderUseCase : IUseCase<CreateOrderRequest, Order>;


    public sealed class GetOrdersUseCase : IGetOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersUseCase(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Task<IEnumerable<Order>> Execute(CreateOrderRequest request)
        {
            try
            {                
                var resultOrders =_orderRepository.GetOrders();              

                return resultOrders;

            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

      
    }


    public record GetOrderRequest();
    public record GetOrderResponse();
}


