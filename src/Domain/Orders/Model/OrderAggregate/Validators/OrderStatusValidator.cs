using Domain.Products.Model.ProductAggregate;
using Domain.SeedWork;

namespace Domain.Orders.Model.OrderAggregate.Validators
{
    

    internal sealed class OrderStatusValidator : IValidator<OrderStatus>
    {
        public bool IsValid(OrderStatus orderStatus, out string error)
        {
            var rule = new IsValidCategory();
            return rule.IsSatisfiedBy(orderStatus, out error);
        }
    }


    internal class IsValidCategory : ISpecification<OrderStatus>
    {
        public bool IsSatisfiedBy(OrderStatus orderStatus, out string error)
        {
            error = "Invalid Order Status - {  PaymentPending = 0, Received = 1, Preparing = 2, Ready = 3 } ";
            return Enum.IsDefined(typeof(OrderStatus.EOrderStatus), orderStatus.Value);
        }
    }
}
