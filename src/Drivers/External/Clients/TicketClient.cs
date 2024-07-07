using Adapters.Gateways.Tickets;

namespace External.Clients;

public class TicketClient : ITicketClient
{
    public Task SendTicket(Ticket ticket)
    {
        // TODO: Produce Event
        throw new NotImplementedException();
    }
}