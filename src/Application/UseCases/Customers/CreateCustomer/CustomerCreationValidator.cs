using Domain.Customer.Model.CustomerAggregate;

namespace Application.UseCases.Customers.CreateCustomer;

public sealed class CustomerCreationValidator(ICustomerRepository customerRepository)
{
    public async Task Validate(CreateCustomerInput input)
    {
        if (await IsCpfAlreadyUsed(input.Cpf))
            throw new ApplicationException("CPF already used");
    }

    private async Task<bool> IsCpfAlreadyUsed(string cpf) => await customerRepository.GetByCpf(cpf) != null;
}