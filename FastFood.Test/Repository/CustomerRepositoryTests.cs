using System;
using System.Threading.Tasks;
using Entities.Customers.CustomerAggregate;
using External.Persistence;
using External.Persistence.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FastFood.Test.Repository
{
    public class CustomerRepositoryTests : IDisposable
    {
        private readonly SqliteConnection _connection;
        private readonly DbContextOptions<FastFoodContext> _options;

        public CustomerRepositoryTests()
        {
            // Configura uma conexão SQLite em memória
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            // Configura as opções do DbContext para usar o SQLite em memória
            _options = new DbContextOptionsBuilder<FastFoodContext>()
                .UseSqlite(_connection)
                .Options;

            // Inicializa o banco de dados com o esquema do DbContext
            using (var context = new FastFoodContext(_options))
            {
                context.Database.EnsureCreated();
            }
        }

        [Fact]
        public async Task GetById_ShouldReturnCustomer_WhenCustomerExists()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var cpf = new Cpf("123.456.789-09"); // Exemplo de CPF
            var customer = new Customer(cpf, "teste", new Email("teste@gmail.com"));

            // Insere o cliente de exemplo no banco de dados SQLite em memória
            using (var context = new FastFoodContext(_options))
            {
                context.Customers.Add(customer);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new FastFoodContext(_options))
            {
                var repository = new CustomerRepository(context);
                var retrievedCustomer = repository.GetById(customer.Id);

                // Assert
                Assert.NotNull(retrievedCustomer);
                Assert.Equal(customer.Id, retrievedCustomer.Id);
                Assert.Equal("teste", retrievedCustomer.Name);
                Assert.Equal(cpf, retrievedCustomer.Cpf);
            }
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            // Arrange: não há necessidade de adicionar clientes ao contexto em memória neste caso

            // Act
            using (var context = new FastFoodContext(_options))
            {
                var repository = new CustomerRepository(context);
                var retrievedCustomer = repository.GetById(Guid.NewGuid());

                // Assert
                Assert.Null(retrievedCustomer);
            }
        }

        [Fact]
        public async Task GetByCpf_ShouldReturnCustomer_WhenCustomerExists()
        {
            // Arrange
            var cpf = new Cpf("123.456.789-09"); // Exemplo de CPF
            var customer = new Customer(cpf, "teste", new Email("teste@gmail.com"));

            // Insere o cliente de exemplo no banco de dados SQLite em memória
            using (var context = new FastFoodContext(_options))
            {
                context.Customers.Add(customer);
                await context.SaveChangesAsync();
            }

            // Act
            using (var context = new FastFoodContext(_options))
            {
                var repository = new CustomerRepository(context);
                var retrievedCustomer = await repository.GetByCpf(cpf);

                // Assert
                Assert.NotNull(retrievedCustomer);
                Assert.Equal("teste", retrievedCustomer.Name);
                Assert.Equal(cpf, retrievedCustomer.Cpf);
            }
        }

        [Fact]
        public async Task GetByCpf_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            // Arrange: não há necessidade de adicionar clientes ao contexto em memória neste caso

            // Act
            using (var context = new FastFoodContext(_options))
            {
                var repository = new CustomerRepository(context);
                var retrievedCustomer = await repository.GetByCpf(new Cpf("123.456.789-09")); // CPF não existente

                // Assert
                Assert.Null(retrievedCustomer);
            }
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
