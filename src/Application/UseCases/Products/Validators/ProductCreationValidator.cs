using Domain.Customer.Model.CustomerAggregate;
using Domain.Product.ProductAggregate;

namespace Application.UseCases.Products.Validators;

public sealed class ProductCreationValidator(IProductRepository productRepository)
{
    public async Task Validate(CreateProductRequest request)
    {
        if (await IsValid(request.Name))
            throw new ApplicationException("Name already used");
    }

    private async Task<bool> IsValid(string name) => false;
}