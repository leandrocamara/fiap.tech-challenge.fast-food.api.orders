using Adapters.Gateways.Notifications;
using Entities.Customers.CustomerAggregate;
using Entities.Orders.OrderAggregate;
using Entities.Products.ProductAggregate;
using Moq;
using Xunit;

public class NotificationGatewayTests
{
    [Fact]
    public void NotifyOrderPaymentUpdate_ValidOrder_CallsClientMethod()
    {
        // Arrange

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
        var order = new Order(costumer, orderItem, 1);
        var mockClient = new Mock<INotificationClient>();
        var gateway = new NotificationGateway(mockClient.Object);
        

        // Act
        gateway.NotifyOrderPaymentUpdate(order);

        // Assert
        mockClient.Verify(x => x.NotifyOrderPaymentUpdate(order), Times.Once);
    }

    [Fact]
    public void NotifyOrderStatusUpdate_ValidOrder_CallsClientMethod()
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
        var order = new Order(costumer, orderItem, 1);
        var mockClient = new Mock<INotificationClient>();
        var gateway = new NotificationGateway(mockClient.Object);
        

        // Act
        gateway.NotifyOrderStatusUpdate(order);

        // Assert
        mockClient.Verify(x => x.NotifyOrderStatusUpdate(order), Times.Once);
    }
}
