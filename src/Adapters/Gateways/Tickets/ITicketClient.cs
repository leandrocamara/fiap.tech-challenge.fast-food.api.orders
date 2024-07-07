namespace Adapters.Gateways.Tickets;

public interface ITicketClient
{
    Task SendTicket(Ticket ticket);

    // TODO: Heartbeat
}

public record Ticket(Guid OrderId, IEnumerable<TicketItem> TicketItems);

public record TicketItem(TicketItemProduct Product, short Quantity);

public record TicketItemProduct(Guid Id, string Name, string Category, string Description);