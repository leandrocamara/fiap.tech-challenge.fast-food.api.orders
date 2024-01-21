using Application.UseCases.Customers;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        #region Customer

        services.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();
        services.AddScoped<IGetCustomerByCpfUseCase, GetCustomerByCpfUseCase>();

        #endregion

        return services;
    }
}