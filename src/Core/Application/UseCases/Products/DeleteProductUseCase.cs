using Entities.Products.ProductAggregate;
using Entities.SeedWork;

namespace Application.UseCases.Products;

public interface IDeleteProductUseCase : IUseCase<DeleteProductRequest, DeleteProductResponse>;

public sealed class DeleteProductUseCase(IProductRepository productRepository) : IDeleteProductUseCase
{  

    public async Task<DeleteProductResponse> Execute(DeleteProductRequest request)
    {
        try
        {
            var product = productRepository.GetById(request.Id);

            if (product is null)
                throw new ApplicationException("Product not found");

            productRepository.Delete(product);

            return new DeleteProductResponse(product.Id);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover the customer. Error: {e.Message}", e);
        }
    }
}

public record DeleteProductRequest(Guid Id);

public record DeleteProductResponse(Guid Id);