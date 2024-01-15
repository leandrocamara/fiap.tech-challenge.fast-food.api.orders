using Domain;
using Domain.Customer.Model.CustomerAggregate;

namespace Application.UseCases.Customers.CreateCustomer;

public interface ICreateCustomerUseCase : IUseCase<CreateCustomerInput, CreateCustomerOutput>;

public sealed class CreateCustomerUseCase : ICreateCustomerUseCase
{
    private readonly ICustomerRepository _customerRepository;
    private readonly CustomerCreationValidator _validator;

    public CreateCustomerUseCase(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
        _validator = new CustomerCreationValidator(_customerRepository);
    }

    public async Task<CreateCustomerOutput> Execute(CreateCustomerInput input)
    {
        try
        {
            await _validator.Validate(input);

            var customer = Customer.New(input.Cpf, input.Name, input.Email);
            await _customerRepository.Save(customer);

            return new CreateCustomerOutput(
                customer.Id,
                customer.Cpf,
                customer.Name,
                customer.Email);
        }
        catch (DomainException)
        {
            throw;
        }
        catch (Exception e)
        {
            throw new ApplicationException("Failed to register the customer", e);
        }
    }
}

public record CreateCustomerInput(string Cpf, string Name, string Email);

public record CreateCustomerOutput(Guid Id, string Cpf, string Name, string Email);