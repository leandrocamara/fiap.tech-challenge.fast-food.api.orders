using Domain.Customers.Model.CustomerAggregate;

namespace Application.UseCases.Customers.Validators;

public sealed class CustomerCreationValidator(ICustomerRepository customerRepository)
{
    public async Task Validate(CreateCustomerRequest request)
    {
        if (await IsCpfAlreadyUsed(request.Cpf))
            throw new ApplicationException("CPF already used");
    }

    private async Task<bool> IsCpfAlreadyUsed(string cpf) => await customerRepository.GetByCpf(cpf) != null;
}