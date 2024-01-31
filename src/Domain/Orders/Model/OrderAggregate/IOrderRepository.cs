using Domain.SeedWork;

namespace Domain.Orders.Model.OrderAggregate
{
    public interface IOrderRepository :IRepository<Order>
    {
        int GetNextOrderNumber();
        Task<IEnumerable<Order>> GetOrders();

        Task<IEnumerable<Order>> GetOrdersTracking(List<OrderStatus> listStatus);
    }
}
