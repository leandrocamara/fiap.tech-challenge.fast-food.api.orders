using Application.UseCases.Customers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ApplicationException = Application.ApplicationException;

namespace API.Controllers;

[ApiController]
[Route("api/customers")]
public class CustomerController : ControllerBase
{
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, "Criar um novo consumidor para identificá-lo em pedidos posteriormente", typeof(CreateCustomerResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erros de validação de dados ou de lógica de negócio, sendo retornado o erro específico no corpo da resposta.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
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
    [SwaggerResponse(StatusCodes.Status200OK, "Retorna o consumidor buscando-o pelo CPF informado.", typeof(GetCustomerByCpfResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caso não encontre consumidor cadastrado pelo CPF informado.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
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