using Adapters.Controllers.Common;
using Application.UseCases.Customers;
using System.Threading.Tasks;

namespace Adapters.Controllers;

public interface ICustomerController
{
    Task<Result> CreateCustomer(CreateCustomerRequest request);
    Task<Result> GetCustomerByCpf(string cpf);
    Task<Result> DisableCustomer(DisableCustomerRequest request);
}

public class CustomerController(
    ICreateCustomerUseCase createCustomerUseCase,
    IGetCustomerByCpfUseCase getCustomerByCpfUseCase,
    IDisableCustomerUseCase disableCustomerUseCase) : BaseController, ICustomerController
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

    public async Task<Result> GetCustomerByCpf(string cpf)
    {
        try
        {
            var response = await Execute(() => getCustomerByCpfUseCase.Execute(cpf));

            return response is null
                ? Result.NotFound()
                : Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }

    public async Task<Result> DisableCustomer(DisableCustomerRequest request)
    {
        try
        {
            var response = await Execute(() => disableCustomerUseCase.Execute(request));
            return Result.Created(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }
}