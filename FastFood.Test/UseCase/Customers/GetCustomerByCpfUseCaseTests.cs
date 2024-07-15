using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using Application.UseCases.Customers;
using Application.Gateways;
using Entities.Customers.CustomerAggregate;
using Entities.SeedWork;
using Application;

namespace FastFood.Test.UseCase.Customers
{
    public class GetCustomerByCpfUseCaseTests
    {
        [Fact]
        public async Task Execute_ExistingCustomer_ReturnsGetCustomerByCpfResponse()
        {
            // Arrange
            var mockCustomerGateway = new Mock<ICustomerGateway>();
            var useCase = new GetCustomerByCpfUseCase(mockCustomerGateway.Object);
            var cpf = "123.456.789-09";
            var existingCustomer = new Customer(cpf, "John Doe", "john.doe@example.com");
            mockCustomerGateway.Setup(g => g.GetByCpf(cpf)).ReturnsAsync(existingCustomer);

            // Act
            var response = await useCase.Execute(cpf);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(existingCustomer.Id, response.Id);
            Assert.Equal(existingCustomer.Cpf, response.Cpf);
            Assert.Equal(existingCustomer.Name, response.Name);
            Assert.Equal(existingCustomer.Email, response.Email);
        }

        [Fact]
        public async Task Execute_NonExistingCustomer_ReturnsNull()
        {
            // Arrange
            var mockCustomerGateway = new Mock<ICustomerGateway>();
            var useCase = new GetCustomerByCpfUseCase(mockCustomerGateway.Object);
            var cpf = "123.456.789-09"; // CPF que não existe no mock
            mockCustomerGateway.Setup(g => g.GetByCpf(cpf)).ReturnsAsync((Customer)null);

            // Act
            var response = await useCase.Execute(cpf);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task Execute_DomainException_ThrowsApplicationException()
        {
            // Arrange
            var mockCustomerGateway = new Mock<ICustomerGateway>();
            var useCase = new GetCustomerByCpfUseCase(mockCustomerGateway.Object);
            var cpf = "123.456.789-09";
            mockCustomerGateway.Setup(g => g.GetByCpf(cpf)).ThrowsAsync(new DomainException("Customer not found"));

            // Act & Assert
            await Assert.ThrowsAsync<Application.ApplicationException>(async () => await useCase.Execute(cpf));
        }
    }
}
