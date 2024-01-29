using Domain.Customers.Model.CustomerAggregate;
using Domain.Orders.Model.OrderAggregate.Validators;
using Domain.SeedWork;

namespace Domain.Orders.Model.OrderAggregate;

public class Order : Entity, IAggregatedRoot
{
    public Customer? Customer { get; private set; }
    public Guid? CustomerId { get; private set; }
    public OrderStatus Status { get; private set; }
    public decimal TotalPrice { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
    private readonly IList<OrderItem> _orderItems;

    public Order(Customer? customer, List<OrderItem> orderItems)
    {
        Id = Guid.NewGuid();
        Customer = customer;
        CustomerId = customer?.Id;
        Status = OrderStatus.PaymentPending();
        CreatedAt = DateTime.UtcNow;
        _orderItems = new List<OrderItem>();

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

    public bool HasItems() => OrderItems.Any();

    // Required for EF
    private Order()
    {
    }
}