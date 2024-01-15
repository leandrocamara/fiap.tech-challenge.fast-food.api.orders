using Domain.Customer.Model.Customer.Validators;

namespace Domain.Customer.Model.Customer;

public readonly struct Cpf
{
    public string Value { get; }

    public Cpf() => Validate();

    public Cpf(string value)
    {
        Value = value;
        Validate();
    }

    private void Validate()
    {
        var validator = new CpfValidator();

        if (validator.IsValid(this) is false)
            throw new DomainException("Invalid CPF");
    }

    public static implicit operator Cpf(string value) => new(value);
}