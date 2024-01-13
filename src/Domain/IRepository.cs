namespace Domain;

public interface IRepository<T> where T : IAggregatedRoot
{
    Task Save(T entity);
    Task<T> GetById(Guid id);
}