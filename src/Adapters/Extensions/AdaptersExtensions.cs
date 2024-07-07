﻿using Adapters.Controllers;
using Adapters.Gateways.Customers;
using Adapters.Gateways.Notifications;
using Adapters.Gateways.Orders;
using Adapters.Gateways.Payments;
using Adapters.Gateways.Products;
using Adapters.Gateways.Tickets;
using Application.Gateways;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.Extensions;

public static class AdaptersExtensions
{
    public static IServiceCollection AddAdaptersDependencies(this IServiceCollection services)
    {
        #region Controllers

        services.AddScoped<ICustomerController, CustomerController>();
        services.AddScoped<IOrderController, OrderController>();
        services.AddScoped<IProductController, ProductController>();

        #endregion

        #region Gateways

        services.AddScoped<ICustomerGateway, CustomerGateway>();
        services.AddScoped<INotificationGateway, NotificationGateway>();
        services.AddScoped<IOrderGateway, OrderGateway>();
        services.AddScoped<IPaymentGateway, PaymentGateway>();
        services.AddScoped<IProductGateway, ProductGateway>();
        services.AddScoped<ITicketGateway, TicketGateway>();

        #endregion

        return services;
    }
}