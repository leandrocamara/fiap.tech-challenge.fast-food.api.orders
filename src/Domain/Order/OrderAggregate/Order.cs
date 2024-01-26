using Domain.Order.OrderAggregate.Validators;
using Domain.SeedWork;

namespace Domain.Order.OrderAggregate
{
    public class Order : Entity, IAggregatedRoot
    {
        public IList<Products.ProductAggregate.Product> OrderItems {get; set; }
        public Domain.Customer.Model.CustomerAggregate.Customer? Customer { get; set; }
        public OrderStatus Status { get; }
        public DateTime CreateAt { get; }
        public double TotalPrice { get; set; }



        public Order(Guid id,IList<Products.ProductAggregate.Product> orderItems, Customer.Model.CustomerAggregate.Customer customer)
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
