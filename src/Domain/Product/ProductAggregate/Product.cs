using Domain.Product.ProductAggregate.Validators;
using Domain.SeedWork;

namespace Domain.Product.ProductAggregate;

public sealed class Product : Entity, IAggregatedRoot
{
    public string Name { get; private set; }
    public Category Category { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }
    //public List<string> Images { get; private set; }

    public Product(Guid id,string name, Category category,decimal price, string description) 
    {
        Id = id;
        Name = name;
        Category = category;
        Price = price;
        Description = description;

        if (Validator.IsValid(this, out var error) is false)
            throw new DomainException(error);
    }

    private static readonly IValidator<Product> Validator = new ProductValidator();
}