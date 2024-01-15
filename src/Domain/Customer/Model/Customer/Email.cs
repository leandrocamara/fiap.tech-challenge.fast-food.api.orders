using Domain.Customer.Model.Customer.Validators;

namespace Domain.Customer.Model.Customer;

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
}