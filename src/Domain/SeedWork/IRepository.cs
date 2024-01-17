namespace Domain.SeedWork;

public interface IRepository<T> where T : IAggregatedRoot
{
    void Save(T entity);
}