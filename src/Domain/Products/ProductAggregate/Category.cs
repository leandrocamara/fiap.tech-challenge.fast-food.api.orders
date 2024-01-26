using Domain.Customers.Model.CustomerAggregate.Validators;
using Domain.Products.ProductAggregate.Validators;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Products.ProductAggregate
{
    public enum ECategory : int
    {
        Meal = 0,
        Side = 1,
        Drink = 2,
        Dessert = 3
    }


    public readonly struct Category
    {
        public ECategory Value { get; }

        public Category(ECategory value)
        {
            Value = value;
            Validate();
        }

        private void Validate()
        {
            if (Validator.IsValid(this, out var error) is false)
                throw new DomainException(error);
        }

        public override string ToString() => Value.ToString();

        // Conversão implícita de Category para int
        public static implicit operator int(Category category) => (int)category.Value;

        // Conversão explícita de int para Category
        public static implicit operator Category(int value) => new((ECategory)value);

        private static readonly IValidator<Category> Validator = new CategoryValidator();
    }
}
