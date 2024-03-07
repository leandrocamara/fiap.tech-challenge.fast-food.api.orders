using Entities.SeedWork;

namespace Entities.Products.ProductAggregate;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategory(Category category);
}