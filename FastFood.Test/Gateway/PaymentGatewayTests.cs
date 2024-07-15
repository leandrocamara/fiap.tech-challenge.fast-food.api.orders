using Adapters.Gateways.Payments;
using Entities.Customers.CustomerAggregate;
using Entities.Orders.OrderAggregate;
using Entities.Products.ProductAggregate;
using Moq;
using Xunit;

public class PaymentGatewayTests
{
    [Fact]
    public void GetQrCodeForPay_ValidOrder_ReturnsQrCodeFromClient()
    {
        // Arrange
        var mockClient = new Mock<IPaymentClient>();
        var gateway = new PaymentGateway(mockClient.Object);
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

        // Mock behavior of IPaymentClient
        string expectedQrCode = "mocked-qr-code";
        mockClient.Setup(x => x.GetQrCodeForPay(order)).Returns(expectedQrCode);

        // Act
        var result = gateway.GetQrCodeForPay(order);

        // Assert
        Assert.Equal(expectedQrCode, result);
        mockClient.Verify(x => x.GetQrCodeForPay(order), Times.Once);
    }
}
