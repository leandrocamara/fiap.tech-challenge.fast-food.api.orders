using Domain.Customer.Model.CustomerAggregate;

namespace Infrastructure.Persistence.Repositories;

public sealed class CustomerRepository : ICustomerRepository
{
    public Task Save(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetByCpf(Cpf cpf)
    {
        throw new NotImplementedException();
    }
}