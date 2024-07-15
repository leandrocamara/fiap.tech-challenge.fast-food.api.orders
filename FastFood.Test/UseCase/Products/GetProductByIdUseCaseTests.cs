using Application.Gateways;
using Application.UseCases.Products;
using Entities.Products.ProductAggregate;
using Entities.SeedWork;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.UseCases.Products
{
    public class GetProductByIdUseCaseTests
    {
        [Fact]
        public async Task Execute_ShouldReturnProductSuccessfully()
        {
            // Arrange
            var images = new List<Image>
            {
                new Image("http://teste.com")
            };
            var productGatewayMock = new Mock<IProductGateway>();
            var productId = Guid.NewGuid();
            var product = new Product(productId, "Test Product", 1, 10.0m, "Description", images);

            productGatewayMock.Setup(g => g.GetById(productId)).Returns(product);

            var useCase = new GetProductByIdUseCase(productGatewayMock.Object);

            // Act
            var response = await useCase.Execute(productId);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(productId, response.Id);
            Assert.Equal("Test Product", response.Name);
            Assert.Equal("Side", response.Category);
            Assert.Equal(10.0m, response.Price);
            Assert.Equal("Description", response.Description);
            Assert.Equal(product.Images, response.images);

            productGatewayMock.Verify(g => g.GetById(productId), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldReturnNull_WhenProductNotFound()
        {
            // Arrange
            var productGatewayMock = new Mock<IProductGateway>();
            var productId = Guid.NewGuid();

            productGatewayMock.Setup(g => g.GetById(productId)).Returns((Product)null);

            var useCase = new GetProductByIdUseCase(productGatewayMock.Object);

            // Act
            var response = await useCase.Execute(productId);

            // Assert
            Assert.Null(response);

            productGatewayMock.Verify(g => g.GetById(productId), Times.Once);
        }

    
    }
}
