using Xunit;
using Moq;
using System;
using System.Threading.Tasks;
using Application.UseCases.Customers;
using Application.UseCases.Customers.Validators;
using Entities.Customers.CustomerAggregate;
using Entities.SeedWork;
using Application.Gateways;
using Application;
using ApplicationException = Application.ApplicationException;

namespace FastFood.Test.UseCase.Customers
{
    public class CreateCustomerUseCaseTests
    {
        [Fact]
        public async Task Execute_ValidCustomer_ReturnsCreateCustomerResponse()
        {
            // Arrange
            var mockCustomerGateway = new Mock<ICustomerGateway>();
            var useCase = new CreateCustomerUseCase(mockCustomerGateway.Object);
            var request = new CreateCustomerRequest("123.456.789-09", "John Doe", "john.doe@example.com");

            // Act
            var response = await useCase.Execute(request);

            // Assert
            Assert.NotNull(response);            
            Assert.Equal(request.Name, response.Name);
            Assert.Equal(request.Email, response.Email);
            mockCustomerGateway.Verify(g => g.Save(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task Execute_InvalidCustomer_ThrowsApplicationException()
        {
            // Arrange
            var mockCustomerGateway = new Mock<ICustomerGateway>();
            var useCase = new CreateCustomerUseCase(mockCustomerGateway.Object);
            var request = new CreateCustomerRequest("", "John Doe", "john.doe@example.com"); // CPF inválido

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(async () => await useCase.Execute(request));
            mockCustomerGateway.Verify(g => g.Save(It.IsAny<Customer>()), Times.Never); // Verifica se o método Save não foi chamado
        }
    }
}
