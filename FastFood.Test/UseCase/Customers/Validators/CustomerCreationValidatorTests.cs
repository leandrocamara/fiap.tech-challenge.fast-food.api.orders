using Xunit;
using Moq;
using System.Threading.Tasks;
using Application.UseCases.Customers.Validators;
using Application.Gateways;
using Application.UseCases.Customers;
using Entities.Customers.CustomerAggregate;

namespace Application.Tests.UseCases.Customers.Validators
{
    public class CustomerCreationValidatorTests
    {
        [Fact]
        public async Task Validate_CpfAlreadyUsed_ThrowsApplicationException()
        {
            // Arrange
            var mockCustomerGateway = new Mock<ICustomerGateway>();
            var validator = new CustomerCreationValidator(mockCustomerGateway.Object);
            var request = new CreateCustomerRequest("123.456.789-09", "John Doe", "john.doe@example.com");
            mockCustomerGateway.Setup(g => g.GetByCpf(request.Cpf)).ReturnsAsync(new Customer(request.Cpf, request.Name, request.Email));

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(async () => await validator.Validate(request));
        }

        [Fact]
        public async Task Validate_CpfNotUsed_PassesValidation()
        {
            // Arrange
            var mockCustomerGateway = new Mock<ICustomerGateway>();
            var validator = new CustomerCreationValidator(mockCustomerGateway.Object);
            var request = new CreateCustomerRequest("123.456.789-09", "John Doe", "john.doe@example.com");
            mockCustomerGateway.Setup(g => g.GetByCpf(request.Cpf)).ReturnsAsync((Customer)null);

            // Act
            await validator.Validate(request);

            // Assert: No exception should be thrown
        }

    }
}

