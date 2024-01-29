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

        public override Order? GetById(Guid id)
        {
            return context.Orders
                .Include(m => m.OrderItems)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(m => m.Id == id);
        }

        public override void Add(Order entity)
        {
            base.Add(entity);
        }

        public int GetNextOrderNumber()
        {
            var today = DateTime.UtcNow.Date;
            return context.Orders
                .Where(m => m.CreatedAt >= today)
                .Count() + 1;
        }
    }
}