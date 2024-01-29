using Domain.Customers.Model.CustomerAggregate;
using Domain.Orders.Model.OrderAggregate.Validators;
using Domain.SeedWork;
using System.ComponentModel.DataAnnotations;

namespace Domain.Orders.Model.OrderAggregate
{
    public class Order : Entity, IAggregatedRoot
    {
        public Guid? CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public IList<OrderItem> OrderItems { get; private set; }

        //private readonly IList<OrderItem> _orderItems;

        public Order(Guid? customerId, List<OrderItem> orderItems)
        {
            OrderItems = orderItems;
            TotalPrice = orderItems.Sum(m => m.TotalPrice);
            CustomerId = customerId;
            if (Validator.IsValid(this, out var error) is false)
                throw new DomainException(error);
        }

        public Order(Customer? customer)
        {
            Id = Guid.NewGuid();
            CustomerId = customer?.Id;
            Status = OrderStatus.PaymentPending();
            OrderItems = new List<OrderItem>();
            CreatedAt = DateTime.UtcNow;

            if (Validator.IsValid(this, out var error) is false)
                throw new DomainException(error);
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            //orderItem.SetOrder(this);
            OrderItems.Add(orderItem);
            TotalPrice += orderItem.TotalPrice;
        }

        private static readonly IValidator<Order> Validator = new OrderValidator();

        public bool HasItems() => OrderItems.Any();

        // Required for EF
        private Order()
        {
        }
    }
}