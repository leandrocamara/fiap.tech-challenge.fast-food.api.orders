using Adapters.Gateways.Customers;
using Adapters.Gateways.Notifications;
using Adapters.Gateways.Orders;
using Adapters.Gateways.Payments;
using Adapters.Gateways.Products;
using Adapters.Gateways.Tickets;
using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using External.Clients.Notifications.OrderStatus;
using External.Clients.Payments;
using External.Clients.Tickets;
using External.HostedServices.Consumers;
using External.Persistence;
using External.Persistence.Migrations;
using External.Persistence.Repositories;
using External.Settings;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace External.Extensions;

public static class ExternalExtensions
{
    public static IServiceCollection AddExternalDependencies(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrdersContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddScoped<IUnitOfWork, OrdersContext>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddHttpClient();
        services.AddScoped<IOrderStatusNotificationClient, OrderStatusNotificationClient>();
        services.AddScoped<IPaymentClient, PaymentClient>();
        services.AddScoped<ITicketClient, TicketClient>();

        services.Configure<PaymentsClientSettings>(configuration.GetSection(nameof(PaymentsClientSettings)));

        SetupAmazonSqs(services, configuration);

        return services;
    }

    private static void SetupAmazonSqs(IServiceCollection services, IConfiguration configuration)
    {
        var settings = GetAmazonSettings(configuration);

        services.AddSingleton<IAmazonSQS>(_ => new AmazonSQSClient(
            new SessionAWSCredentials(settings.AccessKey, settings.SecretKey, settings.SessionToken),
            new AmazonSQSConfig { RegionEndpoint = RegionEndpoint.GetBySystemName(settings.Region) }));

        services.AddHostedService<PaymentUpdatedConsumer>();
        services.AddHostedService<TicketUpdatedConsumer>();
    }

    public static IHealthChecksBuilder AddSqsHealthCheck(
        this IHealthChecksBuilder builder, IConfiguration configuration)
    {
        var settings = GetAmazonSettings(configuration);

        return builder.AddSqs(options =>
        {
            options.Credentials = new SessionAWSCredentials(
                settings.AccessKey,
                settings.SecretKey,
                settings.SessionToken);
            options.RegionEndpoint = RegionEndpoint.GetBySystemName(settings.Region);
        }, name: "sqs_health_check", tags: new[] { "sqs", "healthcheck" });
    }

    private static AmazonSettings GetAmazonSettings(IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(AmazonSettings)).Get<AmazonSettings>();

        if (settings is null)
            throw new ArgumentException($"{nameof(AmazonSettings)} not found.");

        return settings;
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