using Domain.Product.ProductAggregate.Validators;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Order.OrderAggregate.Validators
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
            error = "No product was chosen";            
            
            return order.Product.Any() ;
        }
    }
}
