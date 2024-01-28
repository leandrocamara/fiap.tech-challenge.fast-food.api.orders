using Application.UseCases.Products.Validators;
using Domain.Products.Model.ProductAggregate;
using Domain.SeedWork;

namespace Application.UseCases.Products;

public interface IPutProductUseCase : IUseCase<PutProductRequest, PutProductResponse>;

public sealed class PutProductUseCase : IPutProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly ProductPutValidator _validator;

    public PutProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        _validator = new ProductPutValidator(_productRepository);
    }

    public async Task<PutProductResponse> Execute(PutProductRequest request)
    {
        try
        {
            var product = new Product(request.Id, request.Name, request.Category, request.Price, request.Description, Image.ConvertToImages(request.images));
            
            await _validator.Validate(request);
            _productRepository.Update(product);

            return new PutProductResponse(
                product.Id,               
                product.Name,
                product.Category.ToString(),
                product.Price,
                product.Description,
                Image.ConvertToStrings(product.Images));
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to update the product. Error: {e.Message}", e);
        }
    }
}

public record PutProductRequest(Guid Id, string Name, int Category, decimal Price, string Description, List<string> images);

public record PutProductResponse(Guid Id, string Name, string Category, decimal Price, string Description, List<string> images);