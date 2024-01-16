using Domain.Customer.Model.CustomerAggregate.Validators;
using Domain.SeedWork;

namespace Domain.Customer.Model.CustomerAggregate;

public readonly struct Email
{
    public string Value { get; }

    public Email() => Validate();

    public Email(string value)
    {
        Value = value;
        Validate();
    }

    private void Validate()
    {
        var validator = new EmailValidator();

        if (validator.IsValid(this) is false)
            throw new DomainException("Invalid Email");
    }

    public static implicit operator Email(string value) => new(value);

    public static implicit operator string(Email email) => email.Value;
}