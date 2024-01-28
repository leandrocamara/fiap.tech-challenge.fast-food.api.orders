using Domain.Products.ProductAggregate;
using Domain.SeedWork;

namespace Application.UseCases.Products;

public interface IGetProductsByCategoyUseCase : IUseCase<GetProductsByCategoryRequest, IEnumerable<GetProductsByCategoryResponse>>;

public sealed class GetProductsByCategoyUseCase : IGetProductsByCategoyUseCase
{
    private readonly IProductRepository _productRepository;

    public GetProductsByCategoyUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<GetProductsByCategoryResponse>> Execute(GetProductsByCategoryRequest request)
    {
        try
        {
            var products = await _productRepository.GetByCategory(request.Category);

            if (!products.Any())
                throw new ApplicationException("Products not found");

            return products.Select(product => new GetProductsByCategoryResponse(
                product.Id,
                product.Name,
                product.Category.ToString(),
                product.Price,
                product.Description));
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover products. Error: {e.Message}", e);
        }
    }
}

public record GetProductsByCategoryRequest(int Category);
public record GetProductsByCategoryResponse(Guid Id, string Name, string Category, decimal Price, string Description);