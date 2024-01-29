using Application.UseCases.Orders;
using Application.UseCases.Products;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<CreateOrderResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
        }
    }

    [HttpGet("GetOrderById")]
    [ProducesResponseType<GetOrderByIdResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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