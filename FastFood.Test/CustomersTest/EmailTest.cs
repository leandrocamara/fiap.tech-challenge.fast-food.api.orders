using Xunit;
using Entities.Customers.CustomerAggregate;
using Entities.SeedWork;

namespace Entities.Customers.Tests.CustomerAggregate
{
    public class EmailTests
    {
        [Fact]
        public void Should_Create_Email_With_Valid_Value()
        {
            // Arrange
            var validEmail = "test@example.com";

            // Act
            var email = new Email(validEmail);

            // Assert
            Assert.Equal(validEmail, email.Value);
        }

        [Fact]
        public void Should_Throw_DomainException_For_Invalid_Email()
        {
            // Arrange
            var invalidEmail = "invalid_email";

            // Act & Assert
            Assert.Throws<DomainException>(() => new Email(invalidEmail));
        }

        [Fact]
        public void Should_Implicitly_Convert_From_String_To_Email()
        {
            // Arrange
            var validEmail = "test@example.com";

            // Act
            Email email = validEmail;

            // Assert
            Assert.Equal(validEmail, email.Value);
        }

        [Fact]
        public void Should_Implicitly_Convert_From_Email_To_String()
        {
            // Arrange
            var email = new Email("test@example.com");

            // Act
            string value = email;

            // Assert
            Assert.Equal("test@example.com", value);
        }
    }
}
