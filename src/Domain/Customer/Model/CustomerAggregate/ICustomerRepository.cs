namespace Domain.Customer.Model.CustomerAggregate;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByCpf(Cpf cpf);
}