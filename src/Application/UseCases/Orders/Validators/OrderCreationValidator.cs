using Application.UseCases.Customers;
using Domain.Customers.Model.CustomerAggregate;
using Domain.Products.Model.ProductAggregate;

namespace Application.UseCases.Orders.Validators;

public sealed class OrderCreationValidator(ICustomerRepository customerRepository, IProductRepository productRepository)
{

    public async Task Validate(CreateOrderRequest request)
    {
        if (!await CustomerExistsOrNotInformed(request.CustomerId))
            throw new ApplicationException("Customer not found");

        if (!await ProductsExists(request.OrderItems))
            throw new ApplicationException("Product informed not found");
    }

    private async Task<bool> CustomerExistsOrNotInformed(Guid? customerId)
    {
        if (customerId != null)
            return customerRepository.GetById(customerId.Value) != null;

        return true;
    }

    private async Task<bool> ProductsExists(IEnumerable<CreateOrderItemRequest> OrderItems)
    {
        var result = false;
        foreach (var item in OrderItems)
            result = productRepository.GetById(item.ProductId) != null;

        return result;
    }
}