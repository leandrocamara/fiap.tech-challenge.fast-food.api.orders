using Domain.Customer.Model.CustomerAggregate;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection service, IConfiguration configuration)
    {
        service.AddScoped<ICustomerRepository, CustomerRepository>();

        return service;
    }
}