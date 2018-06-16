using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using TestContext = ECommerce.Integration.Tests.Fixtures.TestContext;

namespace ECommerce.Integration.Tests
{
    [TestFixture]
    public class ProductsControllerIntegerationTests
    {
        private readonly TestContext _sut;

        public ProductsControllerIntegerationTests()
        {
            _sut = new TestContext();
        }

        [Test]
        public async Task ShouldReturnProducts()
        {
            var response = await _sut.Client.GetAsync("/api/products");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(responseString);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            
            Assert.That(products.Count, Is.EqualTo(2));

            Assert.That(products[0].Id, Is.EqualTo(1));
            Assert.That(products[0].Name, Is.EqualTo("Product1"));
            Assert.That(products[0].Price, Is.EqualTo(10));

            Assert.That(products[1].Id, Is.EqualTo(2));
            Assert.That(products[1].Name, Is.EqualTo("Product2"));
            Assert.That(products[1].Price, Is.EqualTo(20));
        }

        [Test]
        public async Task ShouldCreateProduct()
        {

            Product product =  new Product(3, "Product3", 30);
            var productJson = JsonConvert.SerializeObject(product);
            var stringContent = new StringContent(productJson, Encoding.UTF8, "application/json");


            var response = await _sut.Client.PostAsync("/api/products", stringContent);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var productCreated = JsonConvert.DeserializeObject<Product>(responseString);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));


            Assert.That(product.Id, Is.EqualTo(3));
            Assert.That(product.Name, Is.EqualTo("Product3"));
            Assert.That(product.Price, Is.EqualTo(30));
        }
    }
}
