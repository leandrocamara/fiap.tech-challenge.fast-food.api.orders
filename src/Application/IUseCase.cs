namespace Application;

public interface IUseCase<in TInput, TOutput>
{
    Task<TOutput> Execute(TInput input);
}