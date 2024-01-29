using Domain.Customers.Model.CustomerAggregate;
using Domain.Orders.Model.OrderAggregate;
using Domain.SeedWork;

namespace Application.UseCases.Orders
{
    public interface IGetOrderByIdUseCase : IUseCase<GetOrderByIdRequest, GetOrderByIdResponse>;


    public sealed class GetOrderByIdUseCase : IGetOrderByIdUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;

        public GetOrderByIdUseCase(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }

        public async Task<GetOrderByIdResponse> Execute(GetOrderByIdRequest request)
        {

            try
            {
                var order = _orderRepository.GetById(request.Id);

                if (order == null)
                    throw new ApplicationException("Order not found");

                Customer? customer = null;
                if (order.CustomerId.HasValue)
                {
                    customer = _customerRepository.GetById(order.CustomerId.Value);
                }

                return new GetOrderByIdResponse(
                    order.Id, order.OrderNumber, order.CustomerId, customer?.Name, order.Status, order.TotalPrice, 
                    order.OrderItems.Select(item => 
                        new GetOrderByIdOrderItemResponse(item.Id, item.ProductId, item.Product.Name, item.Quantity, item.TotalPrice)
                    ).ToList()
                );
            }
            catch (DomainException e)
            {
                throw new ApplicationException($"Failed to recover product. Error: {e.Message}", e);
            }
        }      
    }

    public record GetOrderByIdRequest(Guid Id);

    public record GetOrderByIdOrderItemResponse(Guid Id, Guid IdProduct, string ProductName, int Quantity, decimal PriceTotal);
    public record GetOrderByIdResponse(Guid Id, int OrderNumber, Guid? CustomerId, string? CustomerName, OrderStatus Status, decimal PriceTotal, IList<GetOrderByIdOrderItemResponse> OrderItems);
}


