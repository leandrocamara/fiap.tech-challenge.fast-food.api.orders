using Application.UseCases.Products.Validators;
using Domain.Products.ProductAggregate;
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
            var product = new Product(request.Name, request.Category, request.Id);
            
            await _validator.Validate(request);
            _productRepository.Update(product);

            return new PutProductResponse(
                product.Id,               
                product.Name,
                product.Category);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to update the product. Error: {e.Message}", e);
        }
    }
}

public record PutProductRequest(Guid Id, string Name, int Category);

public record PutProductResponse(Guid Id, string Name, int Category);