using Domain.SeedWork;

namespace Domain.Products.ProductAggregate;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategory(Category category);
}