using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order.OrderAggregate
{
    public interface IOrderRepository :IRepository<Order>
    {
        Task<Order> GetByOrders();
    }
}
