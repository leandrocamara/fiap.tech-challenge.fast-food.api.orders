namespace Domain.Customer.Model.Customer.Validators;

public sealed class CustomerValidator : IValidator<Customer>
{
    public bool IsValid(Customer customer)
    {
        return string.IsNullOrWhiteSpace(customer.Name) is false;
    }
}