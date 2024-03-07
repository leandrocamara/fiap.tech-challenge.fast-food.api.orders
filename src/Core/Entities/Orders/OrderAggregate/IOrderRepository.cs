using Entities.SeedWork;

namespace Entities.Orders.OrderAggregate;

public interface IOrderRepository :IRepository<Order>
{
    int GetNextOrderNumber();
    Task<IEnumerable<Order>> GetOngoingOrders();
}