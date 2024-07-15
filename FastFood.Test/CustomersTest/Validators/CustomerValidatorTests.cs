using Xunit;
using Entities.Customers.CustomerAggregate.Validators;
using Entities.SeedWork;
using Entities.Customers.CustomerAggregate;

namespace Entities.Tests.Customers.CustomerAggregate.Validators
{
    public class CustomerValidatorTests
    {
        [Fact]
        public void IsValid_ValidCustomer_ReturnsTrue()
        {
            // Arrange
            var validator = new CustomerValidator();
            var validCustomer = new Customer("529.982.247-25", "John Doe",  "john.doe@example.com");
            string error;

            // Act
            var isValid = validator.IsValid(validCustomer, out error);

            // Assert
            Assert.True(isValid);
            
        }

       
    }
}
