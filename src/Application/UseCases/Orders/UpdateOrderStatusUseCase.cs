using Domain.Orders.Model.OrderAggregate;

namespace Application.UseCases.Orders;

public interface IUpdateOrderStatusUseCase : IUseCase<Guid, UpdateOrderStatusResponse>;

public class UpdateOrderStatusUseCase(IOrderRepository orderRepository) : IUpdateOrderStatusUseCase
{
    public Task<UpdateOrderStatusResponse> Execute(Guid orderId)
    {
        var order = orderRepository.GetById(orderId);

        if (order is null)
            throw new ApplicationException("Order not found.");
        
        order.UpdateStatus();
        orderRepository.Update(order);

        return Task.FromResult(new UpdateOrderStatusResponse(order));
    }
}

public record UpdateOrderStatusResponse(Guid Id, string Status)
{
    public UpdateOrderStatusResponse(Order order) : this(order.Id, order.Status)
    {
    }
}