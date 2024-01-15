namespace Domain;

public interface IRepository<T> where T : IAggregatedRoot
{
    Task Save(T entity);
}