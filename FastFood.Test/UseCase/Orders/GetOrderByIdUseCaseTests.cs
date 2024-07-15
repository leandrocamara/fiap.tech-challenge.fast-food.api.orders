using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UseCases.Orders;
using Application.Gateways;
using Entities.Orders.OrderAggregate;
using Xunit;
using Moq;
using Entities.SeedWork;
using Entities.Customers.CustomerAggregate;
using Entities.Products.ProductAggregate;

namespace YourNamespace.Tests
{
    public class GetOrderByIdUseCaseTests
    {
        [Fact]
        public async Task Execute_ReturnsOrderByIdSuccessfully()
        {
            // Arrange
            var orderId = Guid.NewGuid();



            var costumer = new Customer(new Cpf("123.456.789-09"), "danilo queiroz", new("danilo@gmail.com"));
            var images = new List<Image>
            {
                new Image("http://teste.com")
            };

            var product = new Product(Guid.NewGuid(), "teste produto", new Category(Category.ECategory.Drink), 10.0m, "teste descricao", images);
            var orderItem = new List<OrderItem> {
                new OrderItem(product, 1)
            };
            var order = new Order(costumer,orderItem, 1);
            



                var orderGatewayMock = new Mock<IOrderGateway>();
                orderGatewayMock.Setup(g => g.GetById(orderId)).Returns(order);

                var useCase = new GetOrderByIdUseCase(orderGatewayMock.Object);

                // Act
                var result = await useCase.Execute(orderId);

                // Assert
                Assert.NotNull(result);
                
                Assert.Equal(order.OrderNumber, result.Number);
                Assert.Equal(order.CustomerId, result.CustomerId);
                Assert.Equal(order.Customer?.Name, result.Customer);
                Assert.Equal(order.Status.ToString(), result.Status);
                Assert.Equal(order.TotalPrice, result.TotalPrice);
                Assert.Equal(order.OrderItems.Count, result.Items.Count());
                Assert.All(result.Items, item =>
                {
                    var orderItem = order.OrderItems.FirstOrDefault(oi => oi.Id == item.Id);
                    Assert.NotNull(orderItem);
                    Assert.Equal(orderItem.Product.Name, item.Product);
                    Assert.Equal(orderItem.Quantity, item.Quantity);
                    Assert.Equal(orderItem.TotalPrice, item.TotalPrice);
                });
            
        }

        [Fact]
        public async Task Execute_ReturnsNullIfOrderNotFound()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            var orderGatewayMock = new Mock<IOrderGateway>();
            orderGatewayMock.Setup(g => g.GetById(orderId)).Returns((Order)null);

            var useCase = new GetOrderByIdUseCase(orderGatewayMock.Object);

            // Act
            var result = await useCase.Execute(orderId);

            // Assert
            Assert.Null(result);
        }

       
    }
}
