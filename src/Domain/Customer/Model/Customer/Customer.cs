using Domain.Customer.Model.Customer.Validators;

namespace Domain.Customer.Model.Customer;

public sealed class Customer : IAggregatedRoot
{
    public Guid Id { get; set; }
    public Cpf Cpf { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }

    public static Customer New(string cpf, string name, string email)
    {
        var validator = new CustomerValidator();
        var customer = new Customer(Guid.NewGuid(), cpf, name, email);

        if (validator.IsValid(customer) is false)
            throw new DomainException("Invalid Customer");

        return customer;
    }

    private Customer(Guid id, Cpf cpf, string name, Email email)
    {
        Id = id;
        Cpf = cpf;
        Name = name;
        Email = email;
    }
}