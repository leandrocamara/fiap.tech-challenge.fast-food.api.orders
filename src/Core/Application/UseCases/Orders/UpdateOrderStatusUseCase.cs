using Application.Gateways;
using Entities.Orders.OrderAggregate;

namespace Application.UseCases.Orders;

public interface IUpdateOrderStatusUseCase : IUseCase<UpdateOrderStatusRequest, bool>;

public class UpdateOrderStatusUseCase(
    IOrderGateway orderGateway,
    ITicketGateway ticketGateway) : IUpdateOrderStatusUseCase
{
    public async Task<bool> Execute(UpdateOrderStatusRequest request)
    {
        var order = orderGateway.GetById(request.OrderId);

        if (order is null)
            throw new ApplicationException("Order not found.");

        order.UpdateStatus(request.Status);
        orderGateway.Update(order);

        if (order.Status.Equals(OrderStatus.Received()))
            await ticketGateway.CreateTicket(order);

        return true;
    }
}

public record UpdateOrderStatusRequest(Guid OrderId, short Status);