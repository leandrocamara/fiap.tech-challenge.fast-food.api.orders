using Application.ACL.Payment;
using Application.Gateways;
using Entities.Customers.CustomerAggregate;
using Entities.Orders.OrderAggregate;
using Entities.Products.ProductAggregate;
using External.ACL.Payment;
using External.Gateways;
using External.Persistence;
using External.Persistence.Migrations;
using External.Persistence.Repositories;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace External.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureDependencies(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FastFoodContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddScoped<IUnitOfWork, FastFoodContext>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        #region gateways

        services.AddScoped<INotifyGateway, NotifyGateway>();
        services.AddScoped<IPaymentGateway, MercadoPagoQrCodePaymentGateway>();

        #endregion

        return services;
    }

    public static void CreateDatabase(this IApplicationBuilder _, IConfiguration configuration)
    {
        var serviceProvider = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(builder => builder
                .AddPostgres()
                .WithGlobalConnectionString(configuration.GetConnectionString("Default"))
                .ScanIn(typeof(Initial).Assembly).For.Migrations().For.EmbeddedResources())
            .AddLogging(lb => lb.AddFluentMigratorConsole())
            .BuildServiceProvider(false);

        using (serviceProvider.CreateScope())
        {
            
            serviceProvider.GetRequiredService<IMigrationRunner>().MigrateUp();
        }
    }
}