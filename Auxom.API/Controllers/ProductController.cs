using Auxom.Application.DTOs.Product;
using Auxom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auxom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController:ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if(product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync(CreateProductDto product)
        {
            var createdProduct = await _productService.AddProductAsync(product);
            return CreatedAtAction(
                nameof(GetProductById),
                new { id = createdProduct.Id },
                createdProduct
                );
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            var deleted = await _productService.DeleteProductAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(Guid id, UpdateProductDto dto)
        {
            var product = await _productService.UpdateProductAsync(id, dto);

            if (!product)
            {
                return NotFound();
            }
            return Ok("Product updated Succesfully");
        }
    }
}
