namespace Domain.Customer.Model.CustomerAggregate;

public interface ICustomerRepository : IRepository<Customer>
{
    Customer GetByCpf(Cpf cpf);
}