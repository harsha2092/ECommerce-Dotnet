using System;
using System.Collections.Generic;
using System.Text;
using ECommerce.Models;
using ECommerce.Services;
using NUnit.Framework;

namespace ProductsController.Unit.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ProductService _productService;

        [SetUp]
        public void Setup()
        {
            _productService = new ProductService();

        }

        [Test]
        public void ShouldCreateProduct()
        {
            var expectedProduct = new Product(1,"Product1", 10);
            var actualProduct = _productService.Create(expectedProduct);

            Assert.That(expectedProduct.Id, Is.EqualTo(actualProduct.Id));
            Assert.That(expectedProduct.Name, Is.EqualTo(actualProduct.Name));
            Assert.That(expectedProduct.Price, Is.EqualTo(actualProduct.Price));
        }
    }
}
