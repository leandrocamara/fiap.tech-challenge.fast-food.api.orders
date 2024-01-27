using Domain.Customers.Model.CustomerAggregate;
using Domain.Orders.OrderAggregate.Validators;
using Domain.SeedWork;

namespace Domain.Orders.OrderAggregate
{
    public class Order : Entity, IAggregatedRoot
    {
        public IList<OrderItem> OrderItems { get; private set; }
        public Customer Customer { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreateAt { get; private set; }
        public double TotalPrice { get; private set; }


        public Order(Guid id,IList<OrderItem> orderItems, Customer customer, OrderStatus status,DateTime createAt)
        {
            Id = id;
            OrderItems = orderItems;
            Customer = customer;
            Status = status;
            CreateAt = createAt;


            if (Validator.IsValid(this, out var error) is false)
                throw new DomainException(error);
        }

        private static readonly IValidator<Order> Validator = new OrderValidator();

    }
}                                                                           
