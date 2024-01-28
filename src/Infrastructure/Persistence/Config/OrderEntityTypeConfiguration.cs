using Domain.Orders.Model.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Config;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(order => order.Id);

        builder
            .Property(order => order.CreatedAt)
            .IsRequired();

        builder
            .Property(order => order.TotalPrice)
            .IsRequired();

        builder
            .Property(order => order.Status)
            .HasConversion(
                status => (short)status,
                value => value)
            .IsRequired();

        builder
            .HasMany<OrderItem>("_orderItems")
            .WithOne();
    }
}