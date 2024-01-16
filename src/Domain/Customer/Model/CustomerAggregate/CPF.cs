using Domain.Customer.Model.CustomerAggregate.Validators;
using Domain.SeedWork;

namespace Domain.Customer.Model.CustomerAggregate;

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

    public static implicit operator string(Cpf cpf) => cpf.Value;
}