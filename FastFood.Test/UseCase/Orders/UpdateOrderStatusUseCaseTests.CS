using System;
using System.Threading.Tasks;
using Application.UseCases.Orders;
using Application.Gateways;
using Entities.Orders.OrderAggregate;
using Xunit;
using Moq;
using Entities.Customers.CustomerAggregate;
using Entities.Products.ProductAggregate;

namespace YourNamespace.Tests
{
    public class UpdateOrderStatusUseCaseTests
    {
        [Fact]
        public async Task Execute_UpdatesOrderStatusSuccessfully()
        {
            var costumer = new Customer(new Cpf("123.456.789-09"), "danilo queiroz", new("danilo@gmail.com"));
            var images = new List<Image>
            {
                new Image("http://teste.com")
            };

            var product = new Product(Guid.NewGuid(), "teste produto", new Category(Category.ECategory.Drink), 10.0m, "teste descricao", images);
            var orderItem = new List<OrderItem> {
                new OrderItem(product, 1)
            };
            var order = new Order(costumer, orderItem, 1);
            var orderId = Guid.NewGuid();
            
            var orderGatewayMock = new Mock<IOrderGateway>();

            orderGatewayMock.Setup(g => g.GetById(orderId)).Returns(order);

            var useCase = new UpdateOrderStatusUseCase(orderGatewayMock.Object);

            // Act
            var result = await useCase.Execute(orderId);

            // Assert
            Assert.NotNull(result);
           
            Assert.Equal(order.Status.ToString(), result.Status);
            orderGatewayMock.Verify(g => g.Update(order), Times.Once);
        }


    }
}
