using System;
using System.Linq;
using Entities.Customers.CustomerAggregate;
using Entities.SeedWork;
using Xunit;

namespace Entities.Customers.Tests.CustomerAggregate
{
    public class CpfTests
    {
        [Fact]
        public void Constructor_ValidCpf_ShouldSetValue()
        {
            // Arrange
            var validCpf = "12345678909"; // Substitua por um CPF válido de acordo com a lógica do seu CpfValidator

            // Act
            var cpf = new Cpf(validCpf);

            // Assert
            Assert.Equal(validCpf, cpf.Value);
        }

        [Fact]
        public void Constructor_InvalidCpf_ShouldThrowDomainException()
        {
            // Arrange
            var invalidCpf = "123"; // Substitua por um CPF inválido de acordo com a lógica do seu CpfValidator

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => new Cpf(invalidCpf));
            Assert.NotNull(exception);
        }

        [Fact]
        public void Constructor_CpfWithNonDigits_ShouldRemoveNonDigits()
        {
            // Arrange
            var cpfWithNonDigits = "123.456.789-09"; // Formato comum de CPF no Brasil

            // Act
            var cpf = new Cpf(cpfWithNonDigits);

            // Assert
            Assert.Equal("12345678909", cpf.Value);
        }

        [Fact]
        public void ToString_ShouldReturnValue()
        {
            // Arrange
            var validCpf = "12345678909"; // Substitua por um CPF válido de acordo com a lógica do seu CpfValidator
            var cpf = new Cpf(validCpf);

            // Act
            var result = cpf.ToString();

            // Assert
            Assert.Equal(validCpf, result);
        }

        [Fact]
        public void ImplicitConversionFromString_ShouldCreateCpf()
        {
            // Arrange
            var validCpf = "12345678909"; // Substitua por um CPF válido de acordo com a lógica do seu CpfValidator

            // Act
            Cpf cpf = validCpf;

            // Assert
            Assert.Equal(validCpf, cpf.Value);
        }

        [Fact]
        public void ImplicitConversionToString_ShouldReturnValue()
        {
            // Arrange
            var validCpf = "12345678909"; // Substitua por um CPF válido de acordo com a lógica do seu CpfValidator
            var cpf = new Cpf(validCpf);

            // Act
            string result = cpf;

            // Assert
            Assert.Equal(validCpf, result);
        }

        [Fact]
        public void Validate_InvalidCpf_ShouldThrowDomainException()
        {
            // Arrange
            var invalidCpf = "12345678900"; // Substitua por um CPF inválido de acordo com a lógica do seu CpfValidator

            // Act & Assert
            var exception = Assert.Throws<DomainException>(() => new Cpf(invalidCpf));
            Assert.NotNull(exception);
        }
    }
}
