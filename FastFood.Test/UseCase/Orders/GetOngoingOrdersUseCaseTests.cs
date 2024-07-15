using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.UseCases.Orders;
using Application.Gateways;
using Entities.Orders.OrderAggregate;
using Xunit;
using Moq;
using Entities.Customers.CustomerAggregate;
using Entities.SeedWork;
using Entities.Products.ProductAggregate;
using System.Net.Http.Headers;

namespace YourNamespace.Tests
{
    public class GetOngoingOrdersUseCaseTests
    {
        [Fact]
        public async Task Execute_ReturnsOrderResponsesSuccessfully()
        {


            var costumer = new Customer(new Cpf("123.456.789-09"), "danilo queiroz", new("danilo@gmail.com"));
            var images = new List<Image>
            {
                new Image("http://teste.com")
            };

            var product = new Product(Guid.NewGuid(), "teste produto", new Category(Category.ECategory.Drink),10.0m, "teste descricao", images);
            var orderItem = new List<OrderItem> { 
                new OrderItem(product, 1)
            };

            // Arrange
            var orders = new List<Order>
            {
                new Order(costumer,orderItem, 1)
                
            };

            var orderGatewayMock = new Mock<IOrderGateway>();
            orderGatewayMock.Setup(g => g.GetOngoingOrders()).ReturnsAsync(orders);

            var useCase = new GetOngoingOrdersUseCase(orderGatewayMock.Object);

            // Act
            var result = await useCase.Execute();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count()); // Only pending orders should be returned
           
        }

    
    }
}
