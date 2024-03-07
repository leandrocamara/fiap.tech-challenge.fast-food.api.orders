using Entities.SeedWork;

namespace Entities.Customers.CustomerAggregate;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByCpf(Cpf cpf);
}