using Application.UseCases.Customers.Validators;
using Domain.Customers.Model.CustomerAggregate;
using Domain.SeedWork;

namespace Application.UseCases.Customers;

public interface ICreateCustomerUseCase : IUseCase<CreateCustomerRequest, CreateCustomerResponse>;

public sealed class CreateCustomerUseCase : ICreateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly CustomerCreationValidator _validator;

    public CreateCustomerUseCase(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        _validator = new CustomerCreationValidator(_customerRepository);
    }

    public async Task<CreateCustomerResponse> Execute(CreateCustomerRequest request)
    {
        try
        {
            var customer = new Customer(request.Cpf, request.Name, request.Email);

            await _validator.Validate(request);
            _customerRepository.Add(customer);

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