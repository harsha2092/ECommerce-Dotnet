using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Models;
using ECommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProductsController: ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new List<Product>() { new Product(1, "Product1", 10), new Product(2, "Product2", 20) });
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            Product createdProduct = _productService.Create(product);
            return Created($"/api/products/{createdProduct.Id}", createdProduct);
        }
    }
}
