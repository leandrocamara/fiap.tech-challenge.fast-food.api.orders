using Xunit;
using Entities.Customers.CustomerAggregate.Validators;
using Entities.Customers.CustomerAggregate;

namespace YourNamespace.Tests
{
    public class IsValidCpfTests
    {
        private readonly IsValidCpf _cpfValidator = new IsValidCpf();

        [Theory]
        [InlineData("529.982.247-25", true)]    // CPF válido
        [InlineData("123.456.789-09", true)]    // CPF válido
       
        public void IsValidCpf_Validations(string cpfValue, bool expectedValid)
        {
            // Arrange
            var cpf = new Cpf(cpfValue); // Assuming Cpf class exists and is used in IsValidCpf

            // Act
            string error;
            var isValid = _cpfValidator.IsSatisfiedBy(cpf, out error);

            // Assert
            Assert.Equal(expectedValid, isValid);
        }
    }
}
