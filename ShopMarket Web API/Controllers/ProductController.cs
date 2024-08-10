using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMarket_Web_API.Dtos.Product;
using ShopMarket_Web_API.Models;
using ShopMarket_Web_API.Reprositories.ProductReprository;

namespace ShopMarket_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productModel)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);
            
            var result = await _productRepository.CreateAsync(productModel);
            return Ok(result);
        }

        [HttpDelete(("{productId}"))]
        public async Task<IActionResult> Delete([FromRoute] int productId)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState.ToString());

            var ProductDeleted = await _productRepository.DeleteAsync(productId);
            if(ProductDeleted) 
                return Ok($"Product Deleted successfully");

            return NotFound(new { Message = $"Product with ID {productId} was not found." });
        }

        [HttpGet("DeletedProducts")]
        public async Task<IActionResult> GetDeletedProducts()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _productRepository.GetDeletedProductsAsync();
            return Ok(result);
        }
        [HttpGet("StockProducts")]
        public async Task<IActionResult> GetStockProducts()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _productRepository.GetStockProductsAsync();
            return Ok(result);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByID([FromRoute] int productId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _productRepository.GetProductByIdAsync(productId);
            if (result is not null)
                return Ok(result);

            return NotFound(new { Message = $"Product with ID {productId} was not found." });
        }

        [HttpPut("{ProductId}")]
        public async Task<IActionResult> Update([FromRoute] int ProductId,[FromBody] ProductUpdatedDto productDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _productRepository.UpdateAsync(ProductId, productDto);
            return Ok(result);
        }
    }
}
