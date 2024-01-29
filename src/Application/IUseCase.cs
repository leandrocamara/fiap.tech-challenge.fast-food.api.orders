namespace Application;

public interface IUseCase<TResponse>
{
    Task<TResponse> Execute();
}

public interface IUseCase<in TRequest, TResponse>
{
    Task<TResponse> Execute(TRequest request);
}