using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductManager.API.Data;
using ProductManager.API.Dtos;
using ProductManager.API.Entities;

namespace ProductManager.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController:ControllerBase {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductController(ApplicationDbContext context,IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProductsAsync() {
            var products = await _context.Products.AsNoTracking().ToListAsync();
            return Ok(products);
        }

        [Authorize]
        [HttpPost("create-product")]
        public async Task<IActionResult> CreateProductAsync([FromBody] ProductDto productDto) {
            var product = _mapper.Map<Product>(productDto);
            _context.Products.Add(product);

            await _context.SaveChangesAsync();
            return Ok(new { Product = product,Message = "Product Created successfully" });
        }

        [Authorize]
        [HttpPut("update-product")]

        public async Task<IActionResult> UpdateProductAsync([FromBody] UpdateProductDto updateProductDto) {
            var existingProduct = await _context.Products.FindAsync(updateProductDto.Id);
            if(existingProduct == null) {
                return NotFound(new { Message = "Product not found." });
            }

            _mapper.Map(updateProductDto,existingProduct);

            await _context.SaveChangesAsync();
            return Ok(existingProduct);
        }

        [Authorize]
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteProductAsync(string id) {
            if(!Guid.TryParse(id,out var productId)) {
                return BadRequest(new { Message = "Invalid product ID format." });
            }

            var existingProduct = await _context.Products.FindAsync(productId);
            if(existingProduct == null) {
                return NotFound(new { Message = "Product not found." });
            }

            _context.Products.Remove(existingProduct);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Product deleted successfully." });
        }

    }
}
