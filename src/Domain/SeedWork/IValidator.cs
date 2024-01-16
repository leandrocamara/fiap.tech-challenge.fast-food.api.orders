namespace Domain.SeedWork;

public interface IValidator<in T>
{
    bool IsValid(T email);
}

public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T instance);
}

public static class SpecificationExtensionMethods
{
    public static ISpecification<T> And<T>(this ISpecification<T> spec1, ISpecification<T> spec2) =>
        new AndSpecification<T>(spec1, spec2);

    public static ISpecification<T> Or<T>(this ISpecification<T> spec1, ISpecification<T> spec2) =>
        new OrSpecification<T>(spec1, spec2);

    public static ISpecification<T> Not<T>(this ISpecification<T> spec) =>
        new NotSpecification<T>(spec);
}

public class AndSpecification<T>(ISpecification<T> spec1, ISpecification<T> spec2) : ISpecification<T>
{
    private readonly ISpecification<T> _spec1 = spec1 ?? throw new ArgumentNullException(nameof(spec1));
    private readonly ISpecification<T> _spec2 = spec2 ?? throw new ArgumentNullException(nameof(spec2));

    public bool IsSatisfiedBy(T candidate) => _spec1.IsSatisfiedBy(candidate) && _spec2.IsSatisfiedBy(candidate);
}

public class OrSpecification<T>(ISpecification<T> spec1, ISpecification<T> spec2) : ISpecification<T>
{
    private readonly ISpecification<T> _spec1 = spec1 ?? throw new ArgumentNullException(nameof(spec1));
    private readonly ISpecification<T> _spec2 = spec2 ?? throw new ArgumentNullException(nameof(spec2));

    public bool IsSatisfiedBy(T candidate) => _spec1.IsSatisfiedBy(candidate) || _spec2.IsSatisfiedBy(candidate);
}

public class NotSpecification<T>(ISpecification<T> spec) : ISpecification<T>
{
    private readonly ISpecification<T> _spec = spec ?? throw new ArgumentNullException(nameof(spec));

    public bool IsSatisfiedBy(T candidate) => _spec.IsSatisfiedBy(candidate) is false;
}