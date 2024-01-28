using Domain.Orders.OrderAggregate;
using Domain.Products.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Config;

public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("orderItems");

        builder.HasKey(orderItem => orderItem.Id);

        builder
            .Property(orderItem => orderItem.Quantity)
            .IsRequired();

        builder
            .Property(orderItem => orderItem.TotalPrice)
            .IsRequired();

        builder
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(orderItem => orderItem.ProductId)
            .IsRequired();

        builder
            .HasOne<Order>()
            .WithMany()
            .HasForeignKey(orderItem => orderItem.OrderId)
            .IsRequired();
    }
}