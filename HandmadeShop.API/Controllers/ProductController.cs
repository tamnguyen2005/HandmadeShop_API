using HandmadeShop.Application.DTOs.Product;
using HandmadeShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HandmadeShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewProduct(CreateProductRequest request)
        {
            await _productService.CreateProductAsync(request);
            return StatusCode(StatusCodes.Status201Created, new { message = "Create new product successfully !" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] QueryProductRequest request)
        {
            var result = await _productService.GetAllProductAsync(request);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] UpdateProductRequest request)
        {
            await _productService.UpdateProductAsync(id, request);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}