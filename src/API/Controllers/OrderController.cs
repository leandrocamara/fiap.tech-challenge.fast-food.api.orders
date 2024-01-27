using Application.UseCases.Orders;
using Application.UseCases.Products;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType<CreateOrderResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Index([FromServices] ICreateOrderUseCase createOrderUseCase,
        CreateOrderRequest request)
        {
            try
            {
                var response = createOrderUseCase.Execute(request);
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


    }
}
