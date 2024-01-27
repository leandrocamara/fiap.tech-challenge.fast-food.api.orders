using Domain.Products.ProductAggregate;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public abstract class BaseRepository<T>(FastFoodContext context) : IRepository<T> where T : class, IAggregatedRoot 
    {
        public void Add(T entity) => context.Add(entity);

        public void Delete(T entity) => context.Remove(entity);

        public T? GetById(Guid id) => context.Find<T>(id);       

        public void Update(T entity) => context.Entry(entity).State = EntityState.Modified;

    }
}
