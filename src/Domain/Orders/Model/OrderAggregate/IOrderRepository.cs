using Domain.SeedWork;

namespace Domain.Orders.Model.OrderAggregate
{
    public interface IOrderRepository :IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrders();
    }
}
