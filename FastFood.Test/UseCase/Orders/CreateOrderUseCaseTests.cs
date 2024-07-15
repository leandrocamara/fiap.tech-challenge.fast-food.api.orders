using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UseCases.Orders;
using Application.UseCases.Orders.Validators;
using Application.Gateways;
using Entities.Customers.CustomerAggregate;
using Entities.Orders.OrderAggregate;
using Entities.SeedWork;
using Entities.Products.ProductAggregate;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Tests.UseCases.Orders
{
    public class CreateOrderUseCaseTests
    {
      

        [Fact]
        public async Task Execute_EmptyOrderItems_ThrowsApplicationException()
        {
            // Arrange
            var mockOrderGateway = new Mock<IOrderGateway>();
            var mockCustomerGateway = new Mock<ICustomerGateway>();
            var mockProductGateway = new Mock<IProductGateway>();
            var mockPaymentGateway = new Mock<IPaymentGateway>();

            var useCase = new CreateOrderUseCase(
                mockOrderGateway.Object,
                mockCustomerGateway.Object,
                mockProductGateway.Object,
                mockPaymentGateway.Object);

            var request = new CreateOrderRequest(new List<OrderItemRequest>());

            // Act & Assert
            await Assert.ThrowsAsync<ApplicationException>(async () => await useCase.Execute(request));
            mockOrderGateway.Verify(g => g.Save(It.IsAny<Order>()), Times.Never); // Verifica se o método Save não foi chamado
        }
    }
}
