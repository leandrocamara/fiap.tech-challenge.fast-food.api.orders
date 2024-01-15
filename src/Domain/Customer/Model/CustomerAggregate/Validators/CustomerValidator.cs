namespace Domain.Customer.Model.CustomerAggregate.Validators;

public sealed class CustomerValidator : IValidator<CustomerAggregate.Customer>
{
    public bool IsValid(CustomerAggregate.Customer customer)
    {
        return string.IsNullOrWhiteSpace(customer.Name) is false;
    }
}