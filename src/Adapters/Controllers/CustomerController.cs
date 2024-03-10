using Adapters.Controllers.Common;
using Application.UseCases.Customers;

namespace Adapters.Controllers;

public interface ICustomerController
{
    Task<Result> CreateCustomer(CreateCustomerRequest request);
    Task<Result> GetCustomerByCpf(GetCustomerByCpfRequest request);
}

public class CustomerController(
    ICreateCustomerUseCase createCustomerUseCase,
    IGetCustomerByCpfUseCase getCustomerByCpfUseCase) : BaseController, ICustomerController
{
    public async Task<Result> CreateCustomer(CreateCustomerRequest request)
    {
        try
        {
            var response = await Execute(() => createCustomerUseCase.Execute(request));
            return Result.Created(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }

    public async Task<Result> GetCustomerByCpf(GetCustomerByCpfRequest request)
    {
        try
        {
            var response = await Execute(() => getCustomerByCpfUseCase.Execute(request));

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