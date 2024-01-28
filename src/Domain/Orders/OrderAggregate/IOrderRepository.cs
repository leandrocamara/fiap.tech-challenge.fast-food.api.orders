using Domain.SeedWork;

namespace Domain.Orders.OrderAggregate
{
    public interface IOrderRepository :IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrders();
    }
}
