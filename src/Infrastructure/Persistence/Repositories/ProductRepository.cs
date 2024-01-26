using Domain.Product.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class ProductRepository(FastFoodContext context) : IProductRepository
{
    public void Save(Product product) => context.Products.Add(product);    
    public async Task<IEnumerable<Product>> GetByCategory(Category category) =>
        await context.Products.Where(product => product.Category.Equals(category)).ToListAsync();
   
}