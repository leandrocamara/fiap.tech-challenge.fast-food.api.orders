using Domain.Products.ProductAggregate;
using Domain.SeedWork;

namespace Domain.Orders.OrderAggregate
{
    public class OrderItem : Entity
    {
        public Product Product { get; private set; }
        public int Quantity { get; private set; }
        public double PriceTotal { get; private set; }

        public OrderItem(Product product, int quantity )
        {
            Product = product;
            Quantity = quantity;
            PriceTotal = 0;
        }
    }
}
