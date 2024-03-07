namespace Entities.SeedWork;

public interface IRepository<T> where T : IAggregatedRoot
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    T? GetById(Guid id);
}