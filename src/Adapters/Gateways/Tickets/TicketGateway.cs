using Application.Gateways;
using Entities.Orders.OrderAggregate;

namespace Adapters.Gateways.Tickets;

public class TicketGateway(ITicketClient client) : ITicketGateway
{
    public Task CreateTicket(Order order) =>
        client.SendTicket(new Ticket(order.Id, GetTicketItems(order)));

    private IEnumerable<TicketItem> GetTicketItems(Order order) => order.OrderItems.Select(item => new TicketItem(
        new TicketItemProduct(item.Product.Id, item.Product.Name, item.Product.Category, item.Product.Description),
        item.Quantity));
}