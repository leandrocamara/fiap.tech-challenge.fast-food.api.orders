﻿using Adapters.Controllers.Common;
using Application.UseCases.Orders;
using Entities.Orders.OrderAggregate;

namespace Adapters.Controllers;

public interface IOrderController
{
    Task<Result> CreateOrder(CreateOrderRequest request);
    Task<Result> GetOngoingOrders();
    Task<Result> GetOrderById(Guid id);
    Task<Result> UpdatePaymentStatus(Guid orderId, bool paid);
    Task<Result> UpdateOrderStatus(Guid id, OrderStatus status);
}

public class OrderController(
    ICreateOrderUseCase createOrderUseCase,
    IGetOngoingOrdersUseCase getOngoingOrdersUseCase,
    IGetOrderByIdUseCase getOrderByIdUseCase,
    IUpdateOrderStatusUseCase updateOrderStatusUseCase) : BaseController, IOrderController
{
    public async Task<Result> CreateOrder(CreateOrderRequest request)
    {
        try
        {
            var response = await Execute(() => createOrderUseCase.Execute(request));
            return Result.Created(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }

    public async Task<Result> GetOngoingOrders()
    {
        try
        {
            var response = await Execute(getOngoingOrdersUseCase.Execute);
            return Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }

    public async Task<Result> GetOrderById(Guid id)
    {
        try
        {
            var response = await Execute(() => getOrderByIdUseCase.Execute(id));

            return response is null
                ? Result.NotFound()
                : Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }

    public Task<Result> UpdatePaymentStatus(Guid orderId, bool paid)
    {
        var status = paid ? OrderStatus.Received() : OrderStatus.PaymentRefused();
        return UpdateOrderStatus(orderId, status);
    }

    public async Task<Result> UpdateOrderStatus(Guid id, OrderStatus status)
    {
        try
        {
            var request = new UpdateOrderStatusRequest(id, status);
            var response = await Execute(() => updateOrderStatusUseCase.Execute(request));
            return Result.Success(response);
        }
        catch (ControllerException e)
        {
            return e.Result;
        }
    }
}