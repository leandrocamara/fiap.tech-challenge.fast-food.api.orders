using Domain.SeedWork;

namespace Domain.Orders.Model.OrderAggregate;

public interface IOrderRepository :IRepository<Order>
{
    int GetNextOrderNumber();
    Task<IEnumerable<Order>> GetOngoingOrders();
}