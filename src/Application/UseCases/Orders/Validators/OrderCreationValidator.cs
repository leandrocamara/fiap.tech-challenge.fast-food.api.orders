using Domain.Customers.Model.CustomerAggregate;
using Domain.Products.Model.ProductAggregate;

namespace Application.UseCases.Orders.Validators;

public sealed class OrderCreationValidator(ICustomerRepository customerRepository, IProductRepository productRepository)
{
    public async Task Validate(CreateOrderRequest request)
    {
        // TODO: Validations
        await Task.CompletedTask;
    }
}