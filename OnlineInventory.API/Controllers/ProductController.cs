using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineInventory.Application.Interfaces;
using OnlineInventory.Domain.Entities;

namespace OnlineInventory.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public ActionResult<IEnumerable<Product>> GetProductById(int productId)
        {
            var product = _productService.GetProductById(productId);
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> InsertProduct([FromBody] Product product)
        {
            await _productService.InsertProductAsync(product);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            await _productService.UpdateProductAsync(product);
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProductById(int productId)
        {
            await _productService.DeleteProductByIdAsync(productId);
            return Ok();
        }
    }
}