using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Entities.Products.ProductAggregate;
using Entities.SeedWork;

namespace FastFood.Test
{
    public class ImageTests
    {
        [Fact]
        public void Constructor_ShouldInitializeWithValidUrl()
        {
            // Arrange
            var url = "https://validurl.com/image.jpg";

            // Act
            var image = new Image(url);

            // Assert
            Assert.Equal(url, image.Url);
        }


        [Fact]
        public void ConvertToImages_ShouldConvertStringListToImageList()
        {
            // Arrange
            var urls = new List<string>
            {
                "https://validurl.com/image1.jpg",
                "https://validurl.com/image2.jpg"
            };

            // Act
            var images = Image.ConvertToImages(urls);

            // Assert
            Assert.Equal(urls.Count, images.Count);
            for (int i = 0; i < urls.Count; i++)
            {
                Assert.Equal(urls[i], images[i].Url);
            }
        }

        [Fact]
        public void ConvertToStrings_ShouldConvertImageListToStringList()
        {
            // Arrange
            var images = new List<Image>
            {
                new Image("https://validurl.com/image1.jpg"),
                new Image("https://validurl.com/image2.jpg")
            };

            // Act
            var urls = Image.ConvertToStrings(images);

            // Assert
            Assert.Equal(images.Count, urls.Count);
            for (int i = 0; i < images.Count; i++)
            {
                Assert.Equal(images[i].Url, urls[i]);
            }
        }
    }
}
