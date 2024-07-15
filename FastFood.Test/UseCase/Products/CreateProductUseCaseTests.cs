using Application.Gateways;
using Application.UseCases.Products;
using Application.UseCases.Products.Validators;
using Entities.Products.ProductAggregate;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.SeedWork;

namespace Application.Tests.UseCases.Products
{
    public class CreateProductUseCaseTests
    {
        [Fact]
        public async Task Execute_ShouldCreateProductSuccessfully()
        {
            // Arrange
            var productGatewayMock = new Mock<IProductGateway>();
            var validatorMock = new Mock<ProductCreationValidator>(productGatewayMock.Object);

            var request = new CreateProductRequest("Product Name", 1, 100.0m, "Description", new List<string> { "image1", "image2" });

        

            var useCase = new CreateProductUseCase(productGatewayMock.Object);

            // Act
            var response = await useCase.Execute(request);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(request.Name, response.Name);
            
            Assert.Equal(request.Price, response.Price);
            Assert.Equal(request.Description, response.Description);
            Assert.Equal(request.images.Count, response.images.Count);

            productGatewayMock.Verify(g => g.Save(It.IsAny<Product>()), Times.Once);
        }

    }
}
