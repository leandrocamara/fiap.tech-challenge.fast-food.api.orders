using Domain.Customer.Model.CustomerAggregate;
using Domain.Product.ProductAggregate;
using Domain.Products.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Config;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(product => product.Id);

        builder
            .Property(product => product.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(product => product.Category)            
            .IsRequired()
            .HasConversion(
                    v => (int)v,
                    v => (Category)v
                );
    }
}