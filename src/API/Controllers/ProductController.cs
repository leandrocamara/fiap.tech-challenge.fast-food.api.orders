using Application.UseCases.Products;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ApplicationException = Application.ApplicationException;

namespace API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status201Created, "Criar um novo produto para ser usado nos pedidos posteriormente.", typeof(CreateProductResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erros de validação de dados ou de lógica de negócio, sendo retornado o erro específico no corpo da resposta.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> CreateProduct(
        [FromServices] ICreateProductUseCase createProductUseCase,
        CreateProductRequest request)
    {
        try
        {
            var response = await createProductUseCase.Execute(request);
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

    [HttpPut]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Atualizar um produto existente na base de dados.", typeof(PutProductResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erros de validação de dados ou de lógica de negócio, sendo retornado o erro específico no corpo da resposta.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> UpdateProduct(
       [FromServices] IPutProductUseCase useCase,
       PutProductRequest request)
    {
        try
        {
            var response = await useCase.Execute(request);
            return StatusCode(StatusCodes.Status204NoContent, response);
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

    [HttpDelete]
    [SwaggerResponse(StatusCodes.Status202Accepted, "Excluir um produto existente na base de dados.", typeof(DeleteProductResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erros de validação de dados ou de lógica de negócio, sendo retornado o erro específico no corpo da resposta.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> DeleteProduct(
       [FromServices] IDeleteProductUseCase useCase,
       DeleteProductRequest request)
    {
        try
        {
            var response = await useCase.Execute(request);
            return StatusCode(StatusCodes.Status202Accepted, response);
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

    [HttpGet("GetProductsByCategory")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retorna a lista de produtos da categoria informada no parâmetro.", typeof(GetProductsByCategoryResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caso não encontre nenhum produto na categoria informada.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> GetProductsByCategory(
        [FromServices] IGetProductsByCategoryUseCase useCase,
        [FromQuery] GetProductsByCategoryRequest request)
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

    [HttpGet("GetProductById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Retorna o produto pelo id informado no parâmetro.", typeof(GetProductByIdResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caso não encontre o produto informado.")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erros não tratados pelo sistema, sendo retornado o erro específico no corpo da resposta.")]
    public async Task<IActionResult> GetProductsByIdResponse(
        [FromServices] IGetProductByIdUseCase useCase,
        [FromQuery] GetProductByIdRequest request)
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