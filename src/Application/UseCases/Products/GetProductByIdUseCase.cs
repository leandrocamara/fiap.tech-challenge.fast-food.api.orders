using Application.UseCases.Customers;
using Application.UseCases.Products.Validators;
using Domain.Customer.Model.CustomerAggregate;
using Domain.Product.ProductAggregate;
using Domain.SeedWork;

namespace Application.UseCases.Products;

public interface IGetProductByIdUseCase : IUseCase<GetProductByIdRequest, GetProductByIdResponse>;

public sealed class GetProductByIdUseCase(IProductRepository productRepository) : IGetProductByIdUseCase
{
    public Task<GetProductByIdResponse> Execute(GetProductByIdRequest request)
    {
        try
        {
            var product = productRepository.GetById(request.Id);

            if (product == null)
                throw new ApplicationException("Product not found");

            return Task.FromResult(new GetProductByIdResponse(
                product.Id,
                product.Name,
                product.Category.ToString(),
                product.Price,
                product.Description));
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover product. Error: {e.Message}", e);
        }
    }
}

public record GetProductByIdRequest(Guid Id);
public record GetProductByIdResponse(Guid Id, string Name, string Category, decimal Price, string Description);