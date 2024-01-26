using Domain.SeedWork;


namespace Domain.Order.OrderAggregate
{
    public class OrderItem : Entity
    {
        public Products.ProductAggregate.Product Product { get; set; }
    }
}
