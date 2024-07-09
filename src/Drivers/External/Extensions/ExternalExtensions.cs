﻿using Adapters.Gateways.Customers;
using Adapters.Gateways.Notifications;
using Adapters.Gateways.Orders;
using Adapters.Gateways.Payments;
using Adapters.Gateways.Products;
using Adapters.Gateways.Tickets;
using Amazon;
using Amazon.Runtime;
using External.Clients;
using External.Clients.Payments;
using External.Clients.Tickets;
using External.HostedServices;
using External.HostedServices.Consumers;
using External.Persistence;
using External.Persistence.Migrations;
using External.Persistence.Repositories;
using External.Settings;
using FluentMigrator.Runner;
using MassTransit;
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
        services.AddDbContext<FastFoodContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddScoped<IUnitOfWork, FastFoodContext>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddScoped<INotificationClient, NotificationClient>();
        services.AddScoped<IPaymentClient, PaymentClient>();
        services.AddScoped<ITicketClient, TicketClient>();

        services.Configure<PaymentsClientSettings>(configuration.GetSection(nameof(PaymentsClientSettings)));

        SetupAmazonSqs(services, configuration);

        return services;
    }

    private static void SetupAmazonSqs(IServiceCollection services, IConfiguration configuration)
    {
        var settings = GetAmazonSqsSettings(configuration);

        services.AddMassTransit(x =>
        {
            x.AddConsumer<PaymentUpdatedConsumer>();
            x.AddConsumer<TicketUpdatedConsumer>();

            x.UsingAmazonSqs((context, cfg) =>
            {
                cfg.Host(new Uri($"{settings.Host}://{settings.Region}"), h =>
                {
                    h.Credentials(new SessionAWSCredentials(
                        settings.AccessKey,
                        settings.SecretKey,
                        settings.SessionToken));
                });

                cfg.ReceiveEndpoint(PaymentUpdatedConsumer.QueueName,
                    e => { e.ConfigureConsumer<PaymentUpdatedConsumer>(context); });
                cfg.ReceiveEndpoint(TicketUpdatedConsumer.QueueName,
                    e => { e.ConfigureConsumer<TicketUpdatedConsumer>(context); });
            });
        });

        services.AddHostedService<AmazonSqsHostedService>();
    }

    public static IHealthChecksBuilder AddSqsHealthCheck(
        this IHealthChecksBuilder builder, IConfiguration configuration)
    {
        var settings = GetAmazonSqsSettings(configuration);

        return builder.AddSqs(options =>
        {
            options.Credentials = new BasicAWSCredentials(settings.AccessKey, settings.SecretKey);
            options.RegionEndpoint = RegionEndpoint.GetBySystemName(settings.Region);

            options.AddQueue(PaymentUpdatedConsumer.QueueName);
            options.AddQueue(TicketUpdatedConsumer.QueueName);
        }, name: "sqs_health_check", tags: new[] { "sqs", "healthcheck" });
    }

    private static AmazonSqsSettings GetAmazonSqsSettings(IConfiguration configuration)
    {
        var settings = configuration.GetSection(nameof(AmazonSqsSettings)).Get<AmazonSqsSettings>();

        if (settings is null)
            throw new ArgumentException($"{nameof(AmazonSqsSettings)} not found.");

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