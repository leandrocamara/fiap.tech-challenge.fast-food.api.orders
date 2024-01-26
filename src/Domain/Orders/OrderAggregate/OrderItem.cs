using Domain.Products.ProductAggregate;
using Domain.SeedWork;

namespace Domain.Orders.OrderAggregate
{
    public class OrderItem : Entity
    {
        public Product Product { get; set; }
    }
}
