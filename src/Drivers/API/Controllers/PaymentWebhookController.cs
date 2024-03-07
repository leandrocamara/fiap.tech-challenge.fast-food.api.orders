using Application.UseCases.Orders;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[Route("api/payment-update")]
public class PaymentWebhookController : ControllerBase
{
    [HttpPost("mercado-pago-qrcode")]
    [SwaggerResponse(StatusCodes.Status200OK, "WebHook a ser consumido pelo Mercado Pago (meio de pagamento QrCode) para informar atualizações de pagamento.", typeof(UpdatePaymentOrderResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Pedido não encontrado.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> MercadoPagoQrCode(
        [FromServices] IPostUpdatePaymentOrderUseCase updatePaymentUseCase,
        UpdatePaymentOrderRequest request)
    {
        try
        {
            var response = await updatePaymentUseCase.Execute(request);
            return StatusCode(StatusCodes.Status200OK, response);
        }
        catch (ApplicationException e)
        {
            return BadRequest(e.Message + e.StackTrace);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message + e.StackTrace);
        }
    }

}