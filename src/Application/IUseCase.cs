namespace Application;

public interface IUseCase<in TRequest, TResponse>
{
    Task<TResponse> Execute(TRequest input);
}