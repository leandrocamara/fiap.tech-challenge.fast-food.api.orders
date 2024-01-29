using Domain.Orders.Model.OrderAggregate.Validators;
using Domain.SeedWork;

namespace Domain.Orders.Model.OrderAggregate;

public readonly struct OrderStatus
{
    public EOrderStatus Value { get; }

    public static OrderStatus PaymentPending() => new(EOrderStatus.PaymentPending);

    public static implicit operator short(OrderStatus status) => (short)status.Value;

    public static implicit operator OrderStatus(short value) => new((EOrderStatus)value);

    public override string ToString() => Value.ToString();

    private OrderStatus(EOrderStatus status)
    {
        Value = status;
        Validate();
    }

    private void Validate()
    {
        if (Validator.IsValid(this, out var error) is false)
            throw new DomainException(error);
    }

    private static readonly IValidator<OrderStatus> Validator = new OrderStatusValidator();

    public enum EOrderStatus : short
    {
        PaymentPending = 0,
        PaymentRefused = 1,
        Received = 2,
        Preparing = 3,
        Ready = 4
    }
}