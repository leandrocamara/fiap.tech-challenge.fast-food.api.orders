namespace Domain.Customer.Model.CustomerAggregate.Validators;

public sealed class EmailValidator : IValidator<Email>
{
    public bool IsValid(Email email)
    {
        var rule = new IsValidEmail();
        return rule.IsSatisfiedBy(email);
    }
}

internal class IsValidEmail : ISpecification<Email>
{
    public bool IsSatisfiedBy(Email email)
    {
        if (string.IsNullOrWhiteSpace(email.Value))
            return false;

        var index = email.Value.IndexOf('@');

        return
            index > 0 &&
            index != email.Value.Length - 1 &&
            index == email.Value.LastIndexOf('@');
    }
}