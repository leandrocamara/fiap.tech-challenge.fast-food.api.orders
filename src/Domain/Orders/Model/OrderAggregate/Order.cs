using Domain.Customers.Model.CustomerAggregate;
using Domain.Orders.Model.OrderAggregate.Validators;
using Domain.SeedWork;
using static Domain.Orders.Model.OrderAggregate.OrderStatus;

namespace Domain.Orders.Model.OrderAggregate
{
    public class Order : Entity, IAggregatedRoot
    {
        public Guid? CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public int OrderNumber { get; private set; }
        public string? QrCodePayment { get; private set; }
        public DateTime? PaymentStatusDate { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public IList<OrderItem> OrderItems { get; private set; }

        //private readonly IList<OrderItem> _orderItems;

        public Order(Guid? customerId, List<OrderItem> orderItems, int orderNumber)
        {
            Id = Guid.NewGuid();
            OrderItems = orderItems;
            TotalPrice = orderItems.Sum(m => m.TotalPrice);
            CustomerId = customerId;
            OrderNumber = orderNumber;
            if (Validator.IsValid(this, out var error) is false)
                throw new DomainException(error);
        }

        public Order(Customer? customer, int orderNumber)
        {
            Id = Guid.NewGuid();
            CustomerId = customer?.Id;
            Status = OrderStatus.PaymentPending();
            OrderItems = new List<OrderItem>();
            CreatedAt = DateTime.UtcNow;
            OrderNumber = orderNumber;
            if (Validator.IsValid(this, out var error) is false)
                throw new DomainException(error);
        }

        public void AddOrderItem(OrderItem orderItem)
        {
            //orderItem.SetOrder(this);
            OrderItems.Add(orderItem);
            TotalPrice += orderItem.TotalPrice;
        }

        public void SetQrCode(string qrCode)
        {
            QrCodePayment = qrCode;
        }

        public void UpdatePaymentStatus(bool paymentSucceeded)
        {
            Status = paymentSucceeded ? 
                (short)EOrderStatus.Received : 
                (short)EOrderStatus.PaymentRefused;
            PaymentStatusDate = DateTime.UtcNow;
        }

        private static readonly IValidator<Order> Validator = new OrderValidator();

        public bool HasItems() => OrderItems.Any();

        // Required for EF
        private Order()
        {
        }
    }
}