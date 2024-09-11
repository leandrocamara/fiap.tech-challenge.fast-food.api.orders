using Application.Gateways;
using Entities.Customers.CustomerAggregate;

namespace Adapters.Gateways.Customers;

public class CustomerGateway(ICustomerRepository repository) : ICustomerGateway
{
    public void Save(Customer customer) => repository.Add(customer);

    public void Disable(Customer customer)
    {
        customer.Disable();
        repository.Update(customer);        
    }
    public Customer? GetById(Guid id) => repository.GetById(id);

    public Task<Customer?> GetByCpf(Cpf cpf) => repository.GetByCpf(cpf);
}