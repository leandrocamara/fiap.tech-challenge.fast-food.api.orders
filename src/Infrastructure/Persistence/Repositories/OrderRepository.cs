using Domain.Orders.OrderAggregate;

using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Repositories
{
    public sealed class OrderRepository(FastFoodContext context) : BaseRepository<Order>(context), IOrderRepository
    {
        public async Task<IEnumerable<Order>> GetOrders() =>
           await context.Orders.ToListAsync();
   
    }
}
