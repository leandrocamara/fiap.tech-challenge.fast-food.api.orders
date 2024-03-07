using Application.ACL.Payment;
using System.Globalization;
using Entities.Orders.OrderAggregate;

namespace External.ACL.Payment
{
    internal class MercadoPagoQrCodePaymentGateway : IPaymentGateway
    {
        public string GetQrCodeForPay(Order order)
        {
            return string.Format($"OrderId={order.Id}:Valor={order.TotalPrice.ToString(CultureInfo.InvariantCulture)}:Tipo={nameof(MercadoPagoQrCodePaymentGateway)}");
        }
    }
}
