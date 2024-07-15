using Application.Gateways;
using Application.UseCases.Products;
using Entities.Products.ProductAggregate;
using Entities.SeedWork;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Application.Tests.UseCases.Products
{
    public class DeleteProductUseCaseTests
    {
        [Fact]
        public async Task Execute_ShouldDeleteProductSuccessfully()
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

            var useCase = new DeleteProductUseCase(productGatewayMock.Object);

            // Act
            var response = await useCase.Execute(productId);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(productId, response.Id);

            productGatewayMock.Verify(g => g.GetById(productId), Times.Once);
            productGatewayMock.Verify(g => g.Delete(product), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldThrowApplicationException_WhenProductNotFound()
        {
            // Arrange
            var productGatewayMock = new Mock<IProductGateway>();
            var productId = Guid.NewGuid();

            productGatewayMock.Setup(g => g.GetById(productId)).Returns((Product)null);

            var useCase = new DeleteProductUseCase(productGatewayMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ApplicationException>(() => useCase.Execute(productId));

            Assert.Equal("Product not found", exception.Message);

            productGatewayMock.Verify(g => g.GetById(productId), Times.Once);
            productGatewayMock.Verify(g => g.Delete(It.IsAny<Product>()), Times.Never);
        }

    }
}
