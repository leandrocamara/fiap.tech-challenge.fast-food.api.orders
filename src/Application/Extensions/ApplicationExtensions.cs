using Application.UseCases.Customers;
using Application.UseCases.Products;
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

        #region Product

        services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
        services.AddScoped<IPutProductUseCase, PutProductUseCase>();
        services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
        services.AddScoped<IGetProductsByCategoyUseCase, GetProductsByCategoyUseCase>();
        services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();

        #endregion

        return services;
    }
}