using Application.Gateways;
using Entities.Orders.OrderAggregate;
using Entities.SeedWork;

namespace Application.UseCases.Orders
{
    public interface IPostUpdatePaymentOrderUseCase : IUseCase<UpdatePaymentOrderRequest, UpdatePaymentOrderResponse>;


    public sealed class PostUpdatePaymentOrderUseCase : IPostUpdatePaymentOrderUseCase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly INotifyGateway _notifyGateway;


        public PostUpdatePaymentOrderUseCase(IOrderRepository orderRepository, INotifyGateway notifyGateway)
        {
            _orderRepository = orderRepository;
            _notifyGateway = notifyGateway;
        }

        public async Task<UpdatePaymentOrderResponse> Execute(UpdatePaymentOrderRequest request)
        {

            try
            {
                var order = _orderRepository.GetById(request.Id);

                if (order == null)
                    throw new ApplicationException("Order not found");

                order.UpdatePaymentStatus(request.PaymentSucceeded);
                _orderRepository.Update(order);

                _notifyGateway.NotifyOrderPaymentUpdate(order);

                return new UpdatePaymentOrderResponse(order.Id, order.OrderNumber, order.Status);
            }
            catch (DomainException e)
            {
                throw new ApplicationException($"Failed to recover product. Error: {e.Message}", e);
            }
        }      
    }

    public record UpdatePaymentOrderRequest(Guid Id, bool PaymentSucceeded);

    public record UpdatePaymentOrderResponse(Guid Id, int Number, string Status);
}


