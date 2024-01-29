using Application.ACL.Payment;
using Domain.Orders.Model.OrderAggregate;
using System.Globalization;

namespace Infrastructure.ACL.Payment
{
    internal class MercadoPagoQrCodePaymentGateway : IPaymentGateway
    {
        public string GetQrCodeForPay(Order order)
        {
            return string.Format($"OrderId={order.Id}:Valor={order.TotalPrice.ToString(CultureInfo.InvariantCulture)}:Tipo={nameof(MercadoPagoQrCodePaymentGateway)}");
        }
    }
}
