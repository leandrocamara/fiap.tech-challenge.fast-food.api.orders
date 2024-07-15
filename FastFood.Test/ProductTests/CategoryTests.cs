using System;
using Xunit;
using Entities.Products.ProductAggregate;
using Entities.SeedWork;

namespace FastFood.Test
{
    public class CategoryTests
    {
        [Fact]
        public void Constructor_ShouldInitializeWithValidCategory()
        {
            // Arrange
            var categoryValue = Category.ECategory.Meal;

            // Act
            var category = new Category(categoryValue);

            // Assert
            Assert.Equal(categoryValue, category.Value);
        }

        

        [Fact]
        public void ToString_ShouldReturnCategoryName()
        {
            // Arrange
            var category = new Category(Category.ECategory.Meal);

            // Act
            var result = category.ToString();

            // Assert
            Assert.Equal("Meal", result);
        }

        [Fact]
        public void ImplicitConversion_ToInt_ShouldReturnCategoryValueAsInt()
        {
            // Arrange
            var category = new Category(Category.ECategory.Drink);

            // Act
            int result = category;

            // Assert
            Assert.Equal((int)Category.ECategory.Drink, result);
        }

        [Fact]
        public void ExplicitConversion_FromInt_ShouldReturnCategory()
        {
            // Arrange
            int value = (int)Category.ECategory.Side;

            // Act
            var category = (Category)value;

            // Assert
            Assert.Equal(Category.ECategory.Side, category.Value);
        }

        [Theory]
        [InlineData(Category.ECategory.Meal)]
        [InlineData(Category.ECategory.Side)]
        [InlineData(Category.ECategory.Drink)]
        [InlineData(Category.ECategory.Dessert)]
        public void Constructor_ValidCategory_DoesNotThrowException(Category.ECategory category)
        {
            // Arrange & Act
            var exception = Record.Exception(() => new Category(category));

            // Assert
            Assert.Null(exception);
        }

 

        [Theory]
        [InlineData(Category.ECategory.Meal, 0)]
        [InlineData(Category.ECategory.Side, 1)]
        [InlineData(Category.ECategory.Drink, 2)]
        [InlineData(Category.ECategory.Dessert, 3)]
        public void ImplicitConversion_FromCategoryToInt_ReturnsCorrectIntValue(Category.ECategory category, int expectedValue)
        {
            // Arrange
            var categoryStruct = new Category(category);

            // Act
            int intValue = categoryStruct;

            // Assert
            Assert.Equal(expectedValue, intValue);
        }

        [Theory]
        [InlineData(0, Category.ECategory.Meal)]
        [InlineData(1, Category.ECategory.Side)]
        [InlineData(2, Category.ECategory.Drink)]
        [InlineData(3, Category.ECategory.Dessert)]
        public void ExplicitConversion_FromIntToCategory_ReturnsCorrectCategory(int intValue, Category.ECategory expectedCategory)
        {
            // Arrange & Act
            Category categoryStruct = intValue;

            // Assert
            Assert.Equal(expectedCategory, categoryStruct.Value);
        }

        [Theory]
        [InlineData(Category.ECategory.Meal, "Meal")]
        [InlineData(Category.ECategory.Side, "Side")]
        [InlineData(Category.ECategory.Drink, "Drink")]
        [InlineData(Category.ECategory.Dessert, "Dessert")]
        public void ToString_ReturnsCorrectStringRepresentation(Category.ECategory category, string expectedString)
        {
            // Arrange
            var categoryStruct = new Category(category);

            // Act
            var result = categoryStruct.ToString();

            // Assert
            Assert.Equal(expectedString, result);
        }
    }
}
