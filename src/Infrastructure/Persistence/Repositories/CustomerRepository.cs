using Domain.Customer.Model.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Infrastructure.Persistence.Repositories;

public sealed class CustomerRepository(FastFoodContext context) : BaseRepository<Customer>(context), ICustomerRepository
{ 
    public Task<Customer?> GetByCpf(Cpf cpf) =>
        context.Customers.FirstOrDefaultAsync(customer => customer.Cpf.Equals(cpf));
}