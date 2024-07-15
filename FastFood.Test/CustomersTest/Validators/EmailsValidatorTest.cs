using Xunit;
using Entities.Customers.CustomerAggregate.Validators;
using Entities.SeedWork;
using Entities.Customers.CustomerAggregate; // Certifique-se de incluir o namespace onde Email está definido
using Moq;

namespace Entities.Tests.Customers.CustomerAggregate.Validators
{
    public class EmailValidatorTests
    {
        [Fact]
        public void IsValid_ValidEmail_ReturnsTrue()
        {
            // Arrange
            var validator = new EmailValidator();
            var validEmail = new Email("valid@example.com"); // Supondo que Email tenha um construtor que aceita uma string

            // Act
            string error;
            var isValid = validator.IsValid(validEmail, out error);

            // Assert
            Assert.True(isValid);
            
        }
       

 
    }
}
