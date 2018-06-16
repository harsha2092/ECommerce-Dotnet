using System;
using System.Collections.Generic;
using NUnit.Framework;
using ECommerce.Controllers;
using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ProductsController.Unit.Tests
{
    [TestFixture]
    public class ProductsControllerTests
    {
        private ECommerce.Controllers.ProductsController _productsController;
        private Mock<ProductService> _productServiceMock;
        [SetUp]
        public void Setup()
        {
            _productServiceMock = new Mock<ProductService>();
            _productsController = new ECommerce.Controllers.ProductsController(_productServiceMock.Object);
        }

        [Test]
        public void ShouldGetAllProducts()
        {
            var actionResult = _productsController.Get() as OkObjectResult;
            var products = actionResult.Value as List<Product>;
            Assert.That(products.Count, Is.EqualTo(2));

            Assert.That(products[0].Id, Is.EqualTo(1) );
            Assert.That(products[0].Name, Is.EqualTo("Product1") );
            Assert.That(products[0].Price, Is.EqualTo(10) );

            Assert.That(products[1].Id, Is.EqualTo(2));
            Assert.That(products[1].Name, Is.EqualTo("Product2"));
            Assert.That(products[1].Price, Is.EqualTo(20));
        }

        [Test]
        public void ShouldCreateProduct()
        {
            var expectedProduct = new Product(1,"Product1",10);

            _productServiceMock.Setup(productService => productService.Create(expectedProduct)).Returns(expectedProduct).Callback<Product>(product => _assertEqualProduct(product, expectedProduct));

            var createdResult = _productsController.Create(expectedProduct) as CreatedResult;
            var actualProduct = createdResult.Value as Product;
            _productServiceMock.Verify(productService => productService.Create(It.Is<Product>(product => _assertEqualProduct(product, expectedProduct))), Times.Once);
            Assert.That(expectedProduct.Id, Is.EqualTo(actualProduct.Id));
            Assert.That(expectedProduct.Name, Is.EqualTo(actualProduct.Name));
            Assert.That(expectedProduct.Price, Is.EqualTo(actualProduct.Price));
        }

        private bool _assertEqualProduct(Product product, Product otherPoduct)
        {
            return product.Id == otherPoduct.Id && product.Name == otherPoduct.Name &&
                   product.Price == otherPoduct.Price;
        }

    }

}
