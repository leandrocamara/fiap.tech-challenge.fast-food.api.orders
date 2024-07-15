using System;
using System.Collections.Generic;
using Xunit;
using Entities.Products.ProductAggregate;
using Entities.SeedWork;

namespace FastFood.Test
{
    public class ProductTests
    {
        [Fact]
        public void Constructor_ShouldInitializeWithValidParameters()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Burger";
            var category = new Category(Category.ECategory.Meal);
            var price = 9.99m;
            var description = "Delicious burger";
            var images = new List<Image>
            {
                new Image("https://validurl.com/image1.jpg"),
                new Image("https://validurl.com/image2.jpg")
            };

            // Act
            var product = new Product(id, name, category, price, description, images);

            // Assert
            Assert.Equal(id, product.Id);
            Assert.Equal(name, product.Name);
            Assert.Equal(category, product.Category);
            Assert.Equal(price, product.Price);
            Assert.Equal(description, product.Description);
            Assert.Equal(images, product.Images);
        }





    }
}
