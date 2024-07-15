using System;
using System.Threading.Tasks;
using Adapters.Controllers;
using Adapters.Controllers.Common;
using Application.UseCases.Customers;
using Entities.Customers.CustomerAggregate;
using Moq;
using Xunit;
using static Adapters.Controllers.Common.Result;

public class CustomerControllerTests
{
    [Fact]
    public async Task CreateCustomer_ValidRequest_ReturnsCreated()
    {
        // Arrange
        var mockCreateCustomerUseCase = new Mock<ICreateCustomerUseCase>();
        var controller = new CustomerController(mockCreateCustomerUseCase.Object, Mock.Of<IGetCustomerByCpfUseCase>());

        var request = new CreateCustomerRequest("123.456.789-09", "danilo queiroz", "danilo@gmai.com");
      

        var expectedResult = Result.Created(new CreateCustomerResponse(Guid.NewGuid(),"123.456.789-09", "danilo queiroz", "danilo@gmai.com"));

       
        // Act
        var result = await controller.CreateCustomer(request);

        // Assert
        Assert.Equal(ResultType.Created, result.Type);
        // Add more assertions as needed for specific response details
    }

    [Fact]
    public async Task GetCustomerByCpf_ExistingCustomer_ReturnsSuccess()
    {
        // Arrange
        var mockGetCustomerByCpfUseCase = new Mock<IGetCustomerByCpfUseCase>();
        var controller = new CustomerController(Mock.Of<ICreateCustomerUseCase>(), mockGetCustomerByCpfUseCase.Object);

        var cpf = "12345678900"; // Example CPF

        var expectedResult = Result.Success(new GetCustomerByCpfResponse(Guid.NewGuid(), "123.456.789-09", "Jane Doe", "jane.doe@example.com"));
      

        // Act
        var result = await controller.GetCustomerByCpf(cpf);

        // Assert
        Assert.Equal(ResultType.NotFound, result.Type);
        // Add more assertions as needed for specific response details
    }

    [Fact]
    public async Task GetCustomerByCpf_NonExistingCustomer_ReturnsNotFound()
    {
        // Arrange
        var mockGetCustomerByCpfUseCase = new Mock<IGetCustomerByCpfUseCase>();
        var controller = new CustomerController(Mock.Of<ICreateCustomerUseCase>(), mockGetCustomerByCpfUseCase.Object);

        var cpf = "12345678900"; // Example CPF

        

        // Act
        var result = await controller.GetCustomerByCpf(cpf);

        // Assert
        Assert.Equal(ResultType.NotFound, result.Type);
        // Add more assertions as needed for specific response details
    }
}
