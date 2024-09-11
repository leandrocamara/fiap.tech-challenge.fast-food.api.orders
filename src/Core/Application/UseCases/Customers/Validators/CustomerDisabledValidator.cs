using Application.Gateways;

namespace Application.UseCases.Customers.Validators;

public sealed class CustomerDisabledValidator(ICustomerGateway customerGateway)
{
    public async Task Validate(DisableCustomerRequest request)
    {
        if (await IsCpfNotFounded(request.Cpf))
            throw new ApplicationException("CPF not founded");
    }

    private async Task<bool> IsCpfNotFounded(string cpf) => await customerGateway.GetByCpf(cpf) == null;
}