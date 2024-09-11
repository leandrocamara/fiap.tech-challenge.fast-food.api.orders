using Application.Gateways;
using Application.UseCases.Customers.Validators;
using Entities.Customers.CustomerAggregate;
using Entities.SeedWork;

namespace Application.UseCases.Customers;

public interface ICreateCustomerUseCase : IUseCase<CreateCustomerRequest, CreateCustomerResponse>;

public sealed class CreateCustomerUseCase(ICustomerGateway customerGateway) : ICreateCustomerUseCase
{
    private readonly CustomerCreationValidator _validator = new(customerGateway);

    public async Task<CreateCustomerResponse> Execute(CreateCustomerRequest request)
    {
        try
        {
            var customer = new Customer(request.Cpf, request.Name, request.Email);
            customer.Activate();
            await _validator.Validate(request);
            customerGateway.Save(customer);

            return new CreateCustomerResponse(
                customer.Id,
                customer.Cpf,
                customer.Name,
                customer.Email);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to register the customer. Error: {e.Message}", e);
        }
    }
}

public record CreateCustomerRequest(string Cpf, string Name, string Email);

public record CreateCustomerResponse(Guid Id, string Cpf, string Name, string Email);


public interface IDisableCustomerUseCase : IUseCase<DisableCustomerRequest, DisableCustomerResponse>;

public sealed class DisableCustomerUseCase(ICustomerGateway customerGateway) : IDisableCustomerUseCase
{
    private readonly CustomerDisabledValidator _validator = new(customerGateway);

    public async Task<DisableCustomerResponse> Execute(DisableCustomerRequest request)
    {
        try
        {
            var customer = await customerGateway.GetByCpf(request.Cpf);

            await _validator.Validate(request);
            customerGateway.Disable(customer);

            return new DisableCustomerResponse(
                customer.Id,
                customer.Cpf,
                customer.Name,
                customer.Email);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to disable the customer. Error: {e.Message}", e);
        }
    }
}

public record DisableCustomerRequest(string Cpf, string Name, string Email);

public record DisableCustomerResponse(Guid Id, string Cpf, string Name, string Email);