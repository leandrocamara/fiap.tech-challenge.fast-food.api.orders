using Domain.Customers.Model.CustomerAggregate;
using Domain.Orders.Model.OrderAggregate.Validators;
using Domain.SeedWork;
using static Domain.Orders.Model.OrderAggregate.OrderStatus;

namespace Domain.Orders.Model.OrderAggregate
{
    public class Order : Entity, IAggregatedRoot
    {
        public Customer? Customer { get; private set; }
        public Guid? CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public int OrderNumber { get; private set; }
        public string? QrCodePayment { get; private set; }
        public DateTime? PaymentStatusDate { get; private set; }

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        private readonly IList<OrderItem> _orderItems;

        public Order(Customer? customer, List<OrderItem> orderItems, int orderNumber)
        {
            Id = Guid.NewGuid();

            Customer = customer;
            CustomerId = customer?.Id;
            OrderNumber = orderNumber;
            Status = PaymentPending();
            CreatedAt = DateTime.UtcNow;

            _orderItems = new List<OrderItem>();

            foreach (var orderItem in orderItems)
            {
                AddOrderItem(orderItem);
            }

            if (Validator.IsValid(this, out var error) is false)
                throw new DomainException(error);
        }

        private static readonly IValidator<Order> Validator = new OrderValidator();
        public void AddOrderItem(OrderItem orderItem)
        {
            orderItem.SetOrder(this);
            _orderItems.Add(orderItem);
            TotalPrice += orderItem.TotalPrice;
        }

        public void SetQrCode(string qrCode)
        {
            QrCodePayment = qrCode;
        }

        public void UpdatePaymentStatus(bool paymentSucceeded)
        {
            Status = paymentSucceeded ?
                OrderStatus.Received() :
                OrderStatus.PaymentRefused();
            PaymentStatusDate = DateTime.UtcNow;
        }

        public bool HasItems() => OrderItems.Any();

        // Required for EF
        private Order()
        {
        }
    }
}