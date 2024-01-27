using Domain.SeedWork;

namespace Domain.Orders.OrderAggregate.Validators
{
    internal sealed class OrderStatusValidator : IValidator<OrderStatus>
    {
        public bool IsValid(OrderStatus instance, out string error)
        {
            error = "Invalid Status - { Received = 0,Preparing = 1,Ready = 2 } ";
            return Enum.IsDefined(typeof(OrderStatus), instance.Value);
        }
    }
}
