using Application.UseCases.Customers;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = Application.ApplicationException;

namespace API.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<CreateCustomerResponse>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCustomer(
        [FromServices] ICreateCustomerUseCase createCustomerUseCase,
        CreateCustomerRequest request)
    {
        try
        {
            var response = await createCustomerUseCase.Execute(request);
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

    [HttpGet]
    [ProducesResponseType<GetCustomerByCpfResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCustomerByCpf(
        [FromServices] IGetCustomerByCpfUseCase getCustomerByCpfUseCase,
        [FromQuery] GetCustomerByCpfRequest request)
    {
        try
        {
            var response = await getCustomerByCpfUseCase.Execute(request);
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