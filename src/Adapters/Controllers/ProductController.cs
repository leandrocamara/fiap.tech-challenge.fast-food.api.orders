using Adapters.Controllers.Common;
using Application.UseCases.Products;

namespace Adapters.Controllers;

public interface IProductController
{
    Task<Result> CreateProduct(CreateProductRequest request);
    Task<Result> UpdateProduct(PutProductRequest request);
    Task<Result> DeleteProduct(DeleteProductRequest request);
    Task<Result> GetProductsByCategory(GetProductsByCategoryRequest request);
    Task<Result> GetProductById(GetProductByIdRequest request);
}

public class ProductController(
    ICreateProductUseCase createProductUseCase,
    IPutProductUseCase updateProductUseCase,
    IDeleteProductUseCase deleteProductUseCase,
    IGetProductsByCategoryUseCase getProductsByCategoryUseCase,
    IGetProductByIdUseCase getProductByIdUseCase) : BaseController, IProductController
{
    public async Task<Result> CreateProduct(CreateProductRequest request)
    {
        try
        {
            var response = await Execute(() => createProductUseCase.Execute(request));
            return Result.Created(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }

    public async Task<Result> UpdateProduct(PutProductRequest request)
    {
        try
        {
            var response = await Execute(() => updateProductUseCase.Execute(request));
            return Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }

    public async Task<Result> DeleteProduct(DeleteProductRequest request)
    {
        try
        {
            await Execute(() => deleteProductUseCase.Execute(request));
            return Result.Accepted();
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }

    public async Task<Result> GetProductsByCategory(GetProductsByCategoryRequest request)
    {
        try
        {
            var response = await Execute(() => getProductsByCategoryUseCase.Execute(request));
            return Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }

    public async Task<Result> GetProductById(GetProductByIdRequest request)
    {
        try
        {
            var response = await Execute(() => getProductByIdUseCase.Execute(request));

            return response is null
                ? Result.NotFound()
                : Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }
}