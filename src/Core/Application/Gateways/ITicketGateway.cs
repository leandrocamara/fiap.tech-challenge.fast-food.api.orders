using Entities.Orders.OrderAggregate;

namespace Application.Gateways;

public interface ITicketGateway
{
    Task CreateTicket(Order order);
}