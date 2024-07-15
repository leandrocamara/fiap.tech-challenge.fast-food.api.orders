using Application.Gateways;
using Application.UseCases.Products;
using Entities.Products.ProductAggregate;
using Entities.SeedWork;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.UseCases.Products
{
    public class GetProductsByCategoryUseCaseTests
    {
        [Fact]
        public async Task Execute_ShouldReturnProductsSuccessfully()
        {
            // Arrange
            var images = new List<Image>
            {
                new Image("http://teste.com")
            };
            var productGatewayMock = new Mock<IProductGateway>();
            var categoryId = 1;
            var products = new List<Product>
            {
                new Product(Guid.NewGuid(), "Product 1", categoryId, 10.0m, "Description 1",images),
                new Product(Guid.NewGuid(), "Product 2", categoryId, 20.0m, "Description 2", images)
            };

            productGatewayMock.Setup(g => g.GetByCategory(categoryId)).ReturnsAsync(products);

            var useCase = new GetProductsByCategoryUseCase(productGatewayMock.Object);

            // Act
            var response = await useCase.Execute(categoryId);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(2, response.Count());
            Assert.Contains(response, r => r.Name == "Product 1");
            Assert.Contains(response, r => r.Name == "Product 2");

            productGatewayMock.Verify(g => g.GetByCategory(categoryId), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldThrowApplicationException_WhenNoProductsFound()
        {
            // Arrange
            var productGatewayMock = new Mock<IProductGateway>();
            var categoryId = 1;

            productGatewayMock.Setup(g => g.GetByCategory(categoryId)).ReturnsAsync(new List<Product>());

            var useCase = new GetProductsByCategoryUseCase(productGatewayMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApplicationException>(() => useCase.Execute(categoryId));

            Assert.Equal("Products not found", exception.Message);

            productGatewayMock.Verify(g => g.GetByCategory(categoryId), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldThrowApplicationException_WhenDomainExceptionOccurs()
        {
            // Arrange
            var productGatewayMock = new Mock<IProductGateway>();
            var categoryId = 1;

            productGatewayMock.Setup(g => g.GetByCategory(categoryId)).ThrowsAsync(new DomainException("Domain exception"));

            var useCase = new GetProductsByCategoryUseCase(productGatewayMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApplicationException>(() => useCase.Execute(categoryId));

            Assert.Equal("Failed to recover products. Error: Domain exception", exception.Message);

            productGatewayMock.Verify(g => g.GetByCategory(categoryId), Times.Once);
        }
    }
}
