using Domain.SeedWork;

namespace Domain.Customers.Model.CustomerAggregate;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByCpf(Cpf cpf);
}