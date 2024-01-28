using Domain.SeedWork;

namespace Domain.Orders.Model.OrderAggregate.Validators
{
    internal sealed class OrderValidator : IValidator<Order>
    {
        public bool IsValid(Order order, out string error)
        {
            var rule = new Specifications<Order>(
                new IsOrderNameProvided());

            return rule.IsSatisfiedBy(order, out error);
        }
    }

    internal class IsOrderNameProvided : ISpecification<Order>
    {
        public bool IsSatisfiedBy(Order order, out string error)
        {
            // TODO: Fix validation
            // error = "No product was chosen";            
            // return order.OrderItems.Any();
            error = string.Empty;
            return true;
        }
    }
}
