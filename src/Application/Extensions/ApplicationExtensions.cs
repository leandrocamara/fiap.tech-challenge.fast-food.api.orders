using Application.UseCases.Customers;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection service)
    {
        service.AddScoped<ICreateCustomerUseCase, CreateCustomerUseCase>();

        return service;
    }
}