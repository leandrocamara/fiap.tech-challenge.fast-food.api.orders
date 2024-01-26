using Domain.Customers.Model.CustomerAggregate;
using Domain.Products.ProductAggregate;

namespace Application.UseCases.Products.Validators;

public sealed class ProductCreationValidator(IProductRepository productRepository)
{
    public async Task Validate(CreateProductRequest request)
    {
        if (!await IsValid(request.Name))
            throw new ApplicationException("Name already used");
    }

    private async Task<bool> IsValid(string name) => true;
}
