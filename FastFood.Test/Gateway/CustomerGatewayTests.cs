using Adapters.Gateways.Customers;
using Entities.Customers.CustomerAggregate;
using Moq;
using System;
using Xunit;

namespace Adapters.Tests.Gateways.Customers
{
    public class CustomerGatewayTests
    {
        [Fact]
        public void GetById_ExistingCustomerId_ReturnsCustomer()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var mockRepository = new Mock<ICustomerRepository>();
            var expectedCustomer = new Customer(new Cpf("123.456.789-09"),"danilo teste",new Email("danilo@gmail.com"));
            mockRepository.Setup(repo => repo.GetById(customerId)).Returns(expectedCustomer);
            var gateway = new CustomerGateway(mockRepository.Object);

            // Act
            var result = gateway.GetById(customerId);

            // Assert
            Assert.Equal(expectedCustomer, result);
        }

        [Fact]
        public void GetById_NonExistingCustomerId_ReturnsNull()
        {
            // Arrange
            var customerId = Guid.NewGuid();
            var mockRepository = new Mock<ICustomerRepository>();
            mockRepository.Setup(repo => repo.GetById(customerId)).Returns((Customer)null);
            var gateway = new CustomerGateway(mockRepository.Object);

            // Act
            var result = gateway.GetById(customerId);

            // Assert
            Assert.Null(result);
        }
    }
}
