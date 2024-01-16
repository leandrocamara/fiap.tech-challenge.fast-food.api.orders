using Application.UseCases.Customers.Validators;
using Domain.Customer.Model.CustomerAggregate;
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
            await _validator.Validate(request);

            var customer = Customer.New(request.Cpf, request.Name, request.Email);
            await _customerRepository.Save(customer);

            return new CreateCustomerResponse(
                customer.Id,
                customer.Cpf,
                customer.Name,
                customer.Email);
        }
        catch (DomainException e)
        {
            throw new ApplicationException("Failed to register the customer", e);
        }
    }
}

public record CreateCustomerRequest(string Cpf, string Name, string Email);

public record CreateCustomerResponse(Guid Id, string Cpf, string Name, string Email);