﻿using System;
using System.Web.Mvc;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportStore.Domain.Abstract;
using SportStore.Domain.Entities;
using SportStore.WebUI.Controllers;

namespace SportStore.UnitTests
{
    [TestClass]
    public class ImageTest
    {
        [TestMethod]
        public void Can_Retrieve_Image_Data()
        {
            // Arrange - create a Product with image data
            Product prod = new Product
            {
                ProductID = 2,
                Name = "Test",
                ImageData = new byte[] { },
                ImageMimeType = "image/png"
            };
            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                            new Product {ProductID = 1, Name = "P1"},
                            prod,
                            new Product {ProductID = 3, Name = "P3"}
                        }.AsQueryable());
            // Arrange - create the controller
            ProductController target = new ProductController(mock.Object);
            // Act - call the GetImage action method
            ActionResult result = target.GetImage(2);
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(FileResult));
            Assert.AreEqual(prod.ImageMimeType, ((FileResult)result).ContentType);
        }
        [TestMethod]
        public void Cannot_Retrieve_Image_Data_For_Invalid_ID()
        {
            // Arrange - create the mock repository
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[] {
                                new Product {ProductID = 1, Name = "P1"},
                                new Product {ProductID = 2, Name = "P2"}
                            }.AsQueryable());
            // Arrange - create the controller
            ProductController target = new ProductController(mock.Object);
            // Act - call the GetImage action method
            ActionResult result = target.GetImage(100);
            // Assert
            Assert.IsNull(result);
        }
    }
}
