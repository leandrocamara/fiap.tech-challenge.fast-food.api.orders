using Entities.Customers.CustomerAggregate.Validators;
using Entities.Orders.OrderAggregate;
using Entities.SeedWork;

namespace Entities.Customers.CustomerAggregate;

public sealed class Customer : Entity, IAggregatedRoot
{
    public Cpf Cpf { get; private set; }
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public bool Status { get; private set; }

    public Customer(Cpf cpf, string name, Email email)
    {
        Id = Guid.NewGuid();
        Cpf = cpf;
        Name = name;
        Email = email;
        
        if (Validator.IsValid(this, out var error) is false)
            throw new DomainException(error);
    }

    private static readonly IValidator<Customer> Validator = new CustomerValidator();

    // Required for EF
    private Customer()
    {
    }

    public void Disable()
    {
        if (Status)
            Status = false;
        else
            throw new DomainException($"The customer was already disabled");
    }

    public void Activate()
    {
        if (!Status)
            Status = true;
        else
            throw new DomainException($"The customer was already active");
    }
}