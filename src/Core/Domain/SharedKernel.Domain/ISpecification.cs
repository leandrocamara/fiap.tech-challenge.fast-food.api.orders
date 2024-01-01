namespace SharedKernel.Domain;

public interface ISpecification<in T>
{
    bool IsSatisfiedBy(T instance);
}