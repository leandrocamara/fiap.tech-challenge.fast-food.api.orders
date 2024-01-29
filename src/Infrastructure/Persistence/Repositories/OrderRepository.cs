using Domain.Orders.Model.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public sealed class OrderRepository(FastFoodContext context) : BaseRepository<Order>(context), IOrderRepository
    {
        public async Task<IEnumerable<Order>> GetOrders() =>
            await context.Orders.ToListAsync();

        public async Task<IEnumerable<Order>> GetOrdersTracking(List<OrderStatus> listStatus) =>
            await context.Orders.Where(it => listStatus.Contains(it.Status)).ToListAsync();
    }
}