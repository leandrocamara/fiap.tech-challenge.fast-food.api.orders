using Domain.Customer.Model.CustomerAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public sealed class CustomerRepository(FastFoodContext context) : ICustomerRepository
{
    public void Save(Customer customer) => context.Customers.Add(customer);

    public Task<Customer?> GetByCpf(Cpf cpf) =>
        context.Customers.FirstOrDefaultAsync(customer => customer.Cpf.Equals(cpf));
}