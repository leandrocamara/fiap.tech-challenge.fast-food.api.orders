using Domain.Orders.Model.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public sealed class OrderRepository(FastFoodContext context) : BaseRepository<Order>(context), IOrderRepository
    {
        public async Task<IEnumerable<Order>> GetOrders() =>
            await context.Orders.Include(order => order.Customer).ToListAsync();

        public async Task<IEnumerable<Order>> GetOrdersTracking(List<OrderStatus> listStatus) =>
            await context.Orders.Where(order => listStatus.Contains(order.Status)).ToListAsync();

        public override Order? GetById(Guid id)
        {
            return context.Orders
                .Include(order => order.Customer)
                .Include(order => order.OrderItems)
                .ThenInclude(item => item.Product)
                .FirstOrDefault(order => order.Id == id);
        }
    }
}