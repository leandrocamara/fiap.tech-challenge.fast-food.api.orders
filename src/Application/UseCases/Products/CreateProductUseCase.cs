using Application.UseCases.Products.Validators;
using Domain.Products.ProductAggregate;
using Domain.SeedWork;

namespace Application.UseCases.Products;

public interface ICreateProductUseCase : IUseCase<CreateProductRequest, CreateProductResponse>;

public sealed class CreateProductUseCase : ICreateProductUseCase
{
    private readonly IProductRepository _productRepository;
    private readonly ProductCreationValidator _validator;

    public CreateProductUseCase(IProductRepository productRepository)
    {
        _productRepository = productRepository;
        _validator = new ProductCreationValidator(_productRepository);
    }

    public async Task<CreateProductResponse> Execute(CreateProductRequest request)
    {
        try
        {
            var product = new Product(request.Name, request.Category, Guid.NewGuid());

            await _validator.Validate(request);
            _productRepository.Add(product);

            return new CreateProductResponse(
                product.Id,               
                product.Name,
                product.Category.ToString());
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to register the product. Error: {e.Message}", e);
        }
    }
}

public record CreateProductRequest(string Name, int Category);

public record CreateProductResponse(Guid Id, string Name, string Category);