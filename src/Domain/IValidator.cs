namespace Domain;

public interface IValidator<in T>
{
    bool IsValid(T email);
}