using Domain.Products.Model.ProductAggregate;

namespace Application.UseCases.Products.Validators;

public sealed class ProductPutValidator(IProductRepository productRepository)
{
    public async Task Validate(PutProductRequest request)
    {
        if (!await IsValid(request.Name))
            throw new ApplicationException("Name already used");
    }

    private async Task<bool> IsValid(string name) => true;
}