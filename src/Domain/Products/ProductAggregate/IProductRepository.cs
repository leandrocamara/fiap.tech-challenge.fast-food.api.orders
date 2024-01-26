using Domain.SeedWork;

namespace Domain.Product.ProductAggregate;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetByCategory(Category category);
}