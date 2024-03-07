using Entities.Customers.CustomerAggregate;
using Entities.SeedWork;

namespace Application.UseCases.Customers;

public interface IGetCustomerByCpfUseCase : IUseCase<GetCustomerByCpfRequest, GetCustomerByCpfResponse>;

public sealed class GetCustomerByCpfUseCase(ICustomerRepository customerRepository) : IGetCustomerByCpfUseCase
{
    public async Task<GetCustomerByCpfResponse> Execute(GetCustomerByCpfRequest request)
    {
        try
        {
            var customer = await customerRepository.GetByCpf(request.Cpf);

            if (customer is null)
                throw new ApplicationException("Customer not found");

            return new GetCustomerByCpfResponse(
                customer.Id,
                customer.Cpf,
                customer.Name,
                customer.Email);
        }
        catch (DomainException e)
        {
            throw new ApplicationException($"Failed to recover the customer. Error: {e.Message}", e);
        }
    }
}

public record GetCustomerByCpfRequest(string Cpf);

public record GetCustomerByCpfResponse(Guid Id, string Cpf, string Name, string Email);