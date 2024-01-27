using Domain.Orders.OrderAggregate.Validators;
using Domain.SeedWork;

namespace Domain.Orders.OrderAggregate
{

    public enum EOrderStatus
    {
        PaymentPending = 0,
        Received = 1,
        Preparing = 2,
        Ready = 3
    }
    public readonly struct OrderStatus
    {
        public EOrderStatus Value { get; }

        public OrderStatus(EOrderStatus value)
        {
            Value = value;
        }

        private void Validate()
        {
            if (Validator.IsValid(this, out var error) is false)
                throw new DomainException(error);
        }


        private static readonly IValidator<OrderStatus> Validator = new OrderStatusValidator();
        public override string ToString() => Value.ToString();
        public static implicit operator OrderStatus(int value) => new((EOrderStatus)value);
    }
}
