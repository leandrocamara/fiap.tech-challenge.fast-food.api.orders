using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Adapters.Controllers;
using Adapters.Controllers.Common;
using Application.UseCases.Orders;
using Moq;
using Xunit;
using static Adapters.Controllers.Common.Result;

public class OrderControllerTests
{
    [Fact]
    public async Task CreateOrder_ValidRequest_ReturnsCreated()
    {
        // Arrange
        var mockCreateOrderUseCase = new Mock<ICreateOrderUseCase>();
        var controller = new OrderController(
            mockCreateOrderUseCase.Object,
            Mock.Of<IGetOngoingOrdersUseCase>(),
            Mock.Of<IGetOrderByIdUseCase>(),
            Mock.Of<IUpdateOrderStatusUseCase>());


        var orderItem = new List<OrderItemRequest>
        {
            new OrderItemRequest(Guid.NewGuid(),1),
        };

        var request = new CreateOrderRequest(orderItem,Guid.NewGuid());
       



        var expectedResult = Result.Created(request);

       

        // Act
        var result = await controller.CreateOrder(request);

        // Assert
        Assert.Equal(ResultType.Created, result.Type);
        // Add more assertions as needed for specific response details
    }


    [Fact]
    public async Task GetOrderById_ExistingOrder_ReturnsSuccess()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var mockGetOrderByIdUseCase = new Mock<IGetOrderByIdUseCase>();
        var controller = new OrderController(
            Mock.Of<ICreateOrderUseCase>(),
            Mock.Of<IGetOngoingOrdersUseCase>(),
            mockGetOrderByIdUseCase.Object,
            Mock.Of<IUpdateOrderStatusUseCase>());

        var orderItem = new List<OrderItemResponse> {

        new OrderItemResponse(Guid.NewGuid(),"TESTE",1,20.3M)
        
        };
        var expectedOrder = new GetOrderByIdResponse(Guid.NewGuid(), 1, Guid.NewGuid(), "danilo teste", "payment", 23.3m, orderItem);
      

        var expectedResult = Result.Success(expectedOrder);

        mockGetOrderByIdUseCase.Setup(x => x.Execute(orderId))
                               .ReturnsAsync(expectedOrder);

        // Act
        var result = await controller.GetOrderById(orderId);

        // Assert
        Assert.Equal(ResultType.Success, result.Type);
        // Add more assertions as needed for specific response details
    }

    [Fact]
    public async Task GetOrderById_NonExistingOrder_ReturnsNotFound()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var mockGetOrderByIdUseCase = new Mock<IGetOrderByIdUseCase>();
        var controller = new OrderController(
            Mock.Of<ICreateOrderUseCase>(),
            Mock.Of<IGetOngoingOrdersUseCase>(),
            mockGetOrderByIdUseCase.Object,
            Mock.Of<IUpdateOrderStatusUseCase>());

     

        // Act
        var result = await controller.GetOrderById(orderId);

        // Assert
        Assert.Equal(ResultType.NotFound, result.Type);
        // Add more assertions as needed for specific response details
    }


}
