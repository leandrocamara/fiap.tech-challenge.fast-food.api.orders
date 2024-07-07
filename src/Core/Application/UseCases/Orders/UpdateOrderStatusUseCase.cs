using Application.Gateways;

namespace Application.UseCases.Orders;

public interface IUpdateOrderStatusUseCase : IUseCase<UpdateOrderStatusRequest, bool>;

public class UpdateOrderStatusUseCase(IOrderGateway orderGateway) : IUpdateOrderStatusUseCase
{
    public Task<bool> Execute(UpdateOrderStatusRequest request)
    {
        var order = orderGateway.GetById(request.OrderId);

        if (order is null)
            throw new ApplicationException("Order not found.");

        order.UpdateStatus(request.Status);
        orderGateway.Update(order);

        return Task.FromResult(true);
    }
}

public record UpdateOrderStatusRequest(Guid OrderId, short Status);