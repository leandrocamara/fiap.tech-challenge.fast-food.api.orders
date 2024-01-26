using Domain.Products.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class ProductRepository(FastFoodContext context) : BaseRepository<Product>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> GetByCategory(Category category) =>
        await context.Products.Where(product => product.Category.Equals(category)).ToListAsync();
   
}