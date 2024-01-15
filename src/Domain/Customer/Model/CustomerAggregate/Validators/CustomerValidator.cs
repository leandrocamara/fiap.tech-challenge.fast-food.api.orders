namespace Domain.Customer.Model.CustomerAggregate.Validators;

public sealed class CustomerValidator : IValidator<Customer>
{
    public bool IsValid(Customer customer)
    {
        var rule = new IsCustomerNameProvided();
        return rule.IsSatisfiedBy(customer);
    }
}

internal class IsCustomerNameProvided : ISpecification<Customer>
{
    public bool IsSatisfiedBy(Customer customer) => string.IsNullOrWhiteSpace(customer.Name) is false;
}