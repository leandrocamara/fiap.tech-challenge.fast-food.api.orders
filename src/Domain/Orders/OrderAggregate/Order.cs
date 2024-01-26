using Domain.Customers.Model.CustomerAggregate;
using Domain.Orders.OrderAggregate.Validators;
using Domain.Products.ProductAggregate;
using Domain.SeedWork;

namespace Domain.Orders.OrderAggregate
{
    public class Order : Entity, IAggregatedRoot
    {
        public IList<Product> OrderItems {get; set; }
        public Customer? Customer { get; set; }
        public OrderStatus Status { get; }
        public DateTime CreateAt { get; }
        public double TotalPrice { get; set; }



        public Order(Guid id,IList<Product> orderItems, Customer customer)
        {
            Id = id;
            OrderItems = orderItems;
            Customer = customer;

            if (Validator.IsValid(this, out var error) is false)
                throw new DomainException(error);
        }

        private static readonly IValidator<Order> Validator = new OrderValidator();

    }
}                                                                           
