using Application.UseCases.Orders;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, "Criar um novo pedido para o cliente (quando informado) e com os produtos informados. O pedido inicialmente nasce com o status PendingPayment.", typeof(CreateOrderResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erros de validação de dados ou de lógica de negócio, sendo retornado o erro específico no corpo da resposta.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> CreateOrder(
        [FromServices] ICreateOrderUseCase createOrderUseCase,
        CreateOrderRequest request)
    {
        try
        {
            var response = await createOrderUseCase.Execute(request);
            return StatusCode(StatusCodes.Status201Created, response);
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

    [HttpGet("GetOrders")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retorna a lista completa de pedidos existentes.", typeof(GetOrderResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caso não encontre nenhum pedido.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> GetOrders(
        [FromServices] IGetOrdersUseCase useCase,
        [FromQuery] GetOrderRequest request)
    {
        try
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
        catch (ApplicationException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("GetOrderById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retorna o pedido buscando pelo id informado.", typeof(GetOrderByIdResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caso não encontre o pedido informado.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> GetOrderByIdResponse(
        [FromServices] IGetOrderByIdUseCase useCase,
        [FromQuery] GetOrderByIdRequest request)
    {
        try
        {
            var response = await useCase.Execute(request);
            return Ok(response);
        }
        catch (ApplicationException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }
}