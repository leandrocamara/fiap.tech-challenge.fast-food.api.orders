using Domain.SeedWork;

namespace Domain.Product.ProductAggregate.Validators;

internal sealed class ProductValidator : IValidator<Product>
{
    public bool IsValid(Product product, out string error)
    {
        var rule = new Specifications<Product>(
            new IsProductNameProvided());

        return rule.IsSatisfiedBy(product, out error);
    }
}

internal class IsProductNameProvided : ISpecification<Product>
{
    public bool IsSatisfiedBy(Product product, out string error)
    {
        error = "Name not provided";
        return string.IsNullOrWhiteSpace(product.Name) is false;
    }
}

