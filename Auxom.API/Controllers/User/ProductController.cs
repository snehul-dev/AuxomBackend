using AutoMapper;
using Auxom.API.Requests.Product;
using Auxom.Application.DTOs.Product;
using Auxom.Application.Interfaces.Services;
using Auxom.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auxom.API.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public ProductController(IProductService productService, IMapper mapper, IImageService imageService)
        {
            _productService = productService;
            _mapper = mapper;
            _imageService = imageService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            return Ok(product);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProductAsync([FromForm] CreateProductRequest request)
        {
            if (request.Image == null || request.Image.Length == 0)
            {
                return BadRequest("Product image is required.");
            }
            string imageUrl = await _imageService.UploadImageAsync(
                request.Image.OpenReadStream(),
                request.Image.FileName,
                request.Image.ContentType,
                "products"
                );
            var dto = _mapper.Map<CreateProductDto>(request);
            dto.Image = imageUrl;

            var createdProduct = await _productService.AddProductAsync(dto);
            return CreatedAtAction(
                nameof(GetProductById),
                new { id = createdProduct.Id },
                createdProduct
                );
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            await _productService.DeleteProductAsync(id);



            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _productService.GetProductsAsync();
            return Ok(products);

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(Guid id, [FromForm] UpdateProductRequest request)
        {

            var dto = _mapper.Map<UpdateProductDto>(request);
            if (request.Image != null)
            {
                string imageUrl = await _imageService.UploadImageAsync(
                request.Image.OpenReadStream(),
                request.Image.FileName,
                request.Image.ContentType,
                "products"
          );
               
                dto.Image = imageUrl;
             
            }
            await _productService.UpdateProductAsync(id, dto);

            return Ok("Product updated Succesfully");




        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProductsAsync([FromQuery] string keyword)
        {
            var products = await _productService.SearchProductsAsync(keyword);

            return Ok(products);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterProductsAsync([FromQuery] ProductFilter filter)
        {
            var products = await _productService.FilterProductsAsync(filter);
            return Ok(products);
        }
    }
}
