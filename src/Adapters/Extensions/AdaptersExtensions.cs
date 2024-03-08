using Adapters.Gateways.Customers;
using Adapters.Gateways.Notifications;
using Adapters.Gateways.Orders;
using Adapters.Gateways.Payments;
using Adapters.Gateways.Products;
using Application.Gateways;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Extensions;

public static class AdaptersExtensions
{
    public static IServiceCollection AddAdaptersDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICustomerGateway, CustomerGateway>();
        services.AddScoped<INotificationGateway, NotificationGateway>();
        services.AddScoped<IOrderGateway, OrderGateway>();
        services.AddScoped<IPaymentGateway, PaymentGateway>();
        services.AddScoped<IProductGateway, ProductGateway>();

        return services;
    }
}