using Domain.Customers.Model.CustomerAggregate;
using Domain.Orders.Model.OrderAggregate.Validators;
using Domain.SeedWork;

namespace Domain.Orders.Model.OrderAggregate
{
    public class Order : Entity, IAggregatedRoot
    {
        public Guid? CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private readonly IList<OrderItem> _orderItems;

        public Order(Guid? customerId, List<OrderItem> orderItems)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            Status = OrderStatus.PaymentPending();
            _orderItems = new List<OrderItem>();
            CreatedAt = DateTime.UtcNow;

            foreach (var orderItem in orderItems)
            {
                orderItem.SetOrder(this);
                _orderItems.Add(orderItem);
                TotalPrice += orderItem.TotalPrice;
            }

            if (Validator.IsValid(this, out var error) is false)
                throw new DomainException(error);
        }

        private static readonly IValidator<Order> Validator = new OrderValidator();

        public bool HasItems() => _orderItems.Any();

        // Required for EF
        private Order()
        {
        }
    }
}