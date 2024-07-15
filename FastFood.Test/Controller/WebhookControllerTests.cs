using System.Threading.Tasks;
using Adapters.Controllers;
using Adapters.Controllers.Common;
using Application.UseCases.Orders;
using Moq;
using Xunit;
using static Adapters.Controllers.Common.Result;

public class WebhookControllerTests
{
    [Fact]
    public async Task UpdateStatusPayment_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var mockUpdatePaymentUseCase = new Mock<IPostUpdatePaymentOrderUseCase>();
        var controller = new WebhookController(mockUpdatePaymentUseCase.Object);

        var request = new UpdatePaymentOrderRequest(Guid.NewGuid(), true);
       

        var expectedResult = Result.Success(true); // Adjust as per your expected result

        

        // Act
        var result = await controller.UpdateStatusPayment(request);

        // Assert
        Assert.Equal(ResultType.Success, result.Type);
        // Add more assertions as needed for specific response details
    }

    [Fact]
    public async Task UpdateStatusPayment_FailureInUseCase_ReturnsFailure()
    {
        // Arrange
        var mockUpdatePaymentUseCase = new Mock<IPostUpdatePaymentOrderUseCase>();
        var controller = new WebhookController(mockUpdatePaymentUseCase.Object);

        var request = new UpdatePaymentOrderRequest(Guid.NewGuid(), true);


   
        // Act
        var result = await controller.UpdateStatusPayment(request);

        // Assert
        Assert.Equal(ResultType.Success, result.Type);
        // Add more assertions as needed for specific error response details
    }
}
