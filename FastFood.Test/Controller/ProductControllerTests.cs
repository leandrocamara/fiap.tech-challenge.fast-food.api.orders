using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adapters.Controllers;
using Adapters.Controllers.Common;
using Application.UseCases.Products;
using Entities.Products.ProductAggregate;
using Moq;
using Xunit;
using static Adapters.Controllers.Common.Result;

public class ProductControllerTests
{
    [Fact]
    public async Task CreateProduct_ValidRequest_ReturnsCreated()
    {
        // Arrange
        var mockCreateProductUseCase = new Mock<ICreateProductUseCase>();
        var controller = new ProductController(
            mockCreateProductUseCase.Object,
            Mock.Of<IPutProductUseCase>(),
            Mock.Of<IDeleteProductUseCase>(),
            Mock.Of<IGetProductsByCategoryUseCase>(),
            Mock.Of<IGetProductByIdUseCase>());

        var request = new CreateProductRequest("TESTE", 1, 10M, "TESTE", new List<string> { "http://teste.com" });


        var expectedResult = Result.Created(new CreateProductResponse(Guid.NewGuid(), "Product Name", "Category", 10.0m, "Description", new List<Image>()));



        // Act
        var result = await controller.CreateProduct(request);

        // Assert
        Assert.Equal(ResultType.Created, result.Type);
        // Add more assertions as needed for specific response details
    }

    [Fact]
    public async Task UpdateProduct_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var mockUpdateProductUseCase = new Mock<IPutProductUseCase>();
        var controller = new ProductController(
            Mock.Of<ICreateProductUseCase>(),
            mockUpdateProductUseCase.Object,
            Mock.Of<IDeleteProductUseCase>(),
            Mock.Of<IGetProductsByCategoryUseCase>(),
            Mock.Of<IGetProductByIdUseCase>());

        var request = new PutProductRequest(Guid.NewGuid(),"teste",2,22.2m,"teste", new List<string>());
      

        var expectedResult = Result.Success(new PutProductResponse(Guid.NewGuid(), "Updated Product Name", "Category", 20.0m, "Updated Description", new List<Image>()));

       

        // Act
        var result = await controller.UpdateProduct(request);

        // Assert
        Assert.Equal(ResultType.Success, result.Type);
        // Add more assertions as needed for specific response details
    }

    [Fact]
    public async Task DeleteProduct_ValidId_ReturnsAccepted()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var mockDeleteProductUseCase = new Mock<IDeleteProductUseCase>();
        var controller = new ProductController(
            Mock.Of<ICreateProductUseCase>(),
            Mock.Of<IPutProductUseCase>(),
            mockDeleteProductUseCase.Object,
            Mock.Of<IGetProductsByCategoryUseCase>(),
            Mock.Of<IGetProductByIdUseCase>());


        // Act
        var result = await controller.DeleteProduct(productId);

        // Assert
        Assert.Equal(ResultType.Accepted, result.Type);
        // Add more assertions as needed for specific response details
    }

    [Fact]
    public async Task GetProductsByCategory_ReturnsSuccess()
    {
        // Arrange
        var categoryId = 1;
        var mockGetProductsByCategoryUseCase = new Mock<IGetProductsByCategoryUseCase>();
        var controller = new ProductController(
            Mock.Of<ICreateProductUseCase>(),
            Mock.Of<IPutProductUseCase>(),
            Mock.Of<IDeleteProductUseCase>(),
            mockGetProductsByCategoryUseCase.Object,
            Mock.Of<IGetProductByIdUseCase>());

        var products = new List<GetProductsByCategoryResponse>
        {
            // Fill in with sample products
        };

        var expectedResult = Result.Success(products);

        mockGetProductsByCategoryUseCase.Setup(x => x.Execute(categoryId))
                                        .ReturnsAsync(products);

        // Act
        var result = await controller.GetProductsByCategory(categoryId);

        // Assert
        Assert.Equal(ResultType.Success, result.Type);
        // Add more assertions as needed for specific response details
    }

    [Fact]
    public async Task GetProductById_ExistingProduct_ReturnsSuccess()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var mockGetProductByIdUseCase = new Mock<IGetProductByIdUseCase>();
        var controller = new ProductController(
            Mock.Of<ICreateProductUseCase>(),
            Mock.Of<IPutProductUseCase>(),
            Mock.Of<IDeleteProductUseCase>(),
            Mock.Of<IGetProductsByCategoryUseCase>(),
            mockGetProductByIdUseCase.Object);

        var expectedProduct = new GetProductByIdResponse(Guid.NewGuid(), "Updated Product Name", "Category", 20.0m, "Updated Description", new List<Image>());
        

        var expectedResult = Result.Success(expectedProduct);

        mockGetProductByIdUseCase.Setup(x => x.Execute(productId))
                                 .ReturnsAsync(expectedProduct);

        // Act
        var result = await controller.GetProductById(productId);

        // Assert
        Assert.Equal(ResultType.Success, result.Type);
        // Add more assertions as needed for specific response details
    }

    [Fact]
    public async Task GetProductById_NonExistingProduct_ReturnsNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var mockGetProductByIdUseCase = new Mock<IGetProductByIdUseCase>();
        var controller = new ProductController(
            Mock.Of<ICreateProductUseCase>(),
            Mock.Of<IPutProductUseCase>(),
            Mock.Of<IDeleteProductUseCase>(),
            Mock.Of<IGetProductsByCategoryUseCase>(),
            mockGetProductByIdUseCase.Object);

      

        // Act
        var result = await controller.GetProductById(productId);

        // Assert
        Assert.Equal(ResultType.NotFound, result.Type);
        // Add more assertions as needed for specific response details
    }
}
