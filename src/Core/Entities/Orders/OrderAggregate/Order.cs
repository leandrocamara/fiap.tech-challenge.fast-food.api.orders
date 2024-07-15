﻿using Entities.Customers.CustomerAggregate;
using Entities.Orders.OrderAggregate.Validators;
using Entities.SeedWork;

namespace Entities.Orders.OrderAggregate
{
    public class Order : Entity, IAggregatedRoot
    {
        public Customer? Customer { get; private set; }
        public Guid? CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public int OrderNumber { get; private set; }

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
        private readonly IList<OrderItem> _orderItems;

        public Order(Customer? customer, List<OrderItem> orderItems, int orderNumber)
        {
            Id = Guid.NewGuid();

            Customer = customer;
            CustomerId = customer?.Id;
            OrderNumber = orderNumber;
            Status = OrderStatus.PaymentPending();
            CreatedAt = DateTime.UtcNow;

            _orderItems = new List<OrderItem>();

            foreach (var orderItem in orderItems)
                AddOrderItem(orderItem);

            if (Validator.IsValid(this, out var error) is false)
                throw new DomainException(error);
        }

        public bool IsEmpty() => OrderItems.Any();

        public void UpdateStatus(OrderStatus status)
        {
            if (StatusSequence.TryGetValue(Status, out var nextStatus) && nextStatus.Contains(status))
                Status = status;
            else
                throw new DomainException($"Changing the status from {Status} to {status} is not allowed.");
        }

        private void AddOrderItem(OrderItem orderItem)
        {
            orderItem.SetOrder(this);
            _orderItems.Add(orderItem);
            TotalPrice += orderItem.TotalPrice;
        }

        private static readonly Dictionary<OrderStatus, OrderStatus[]> StatusSequence = new()
        {
            { OrderStatus.PaymentPending(), [OrderStatus.Received(), OrderStatus.PaymentRefused()] },
            { OrderStatus.Received(), [OrderStatus.Preparing()] },
            { OrderStatus.Preparing(), [OrderStatus.Ready()] },
            { OrderStatus.Ready(), [OrderStatus.Completed()] }
        };

        private static readonly IValidator<Order> Validator = new OrderValidator();

        // Required for EF
        private Order()
        {
        }
    }
}