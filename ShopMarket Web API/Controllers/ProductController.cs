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
            return Ok(_mapper.Map<ProductCreateDto>(result));
        }

        [HttpDelete(("{productId}"))]
        public async Task<IActionResult> Delete([FromRoute] int productId)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState.ToString());

            var ProductResult = await _productRepository.DeleteAsync(productId);
            if(ProductResult != null) 
                return Ok($"Product with Name : {ProductResult.Name} Deleted");

            return BadRequest("Product Id not correct");
        }

        [HttpGet("DeletedProducts")]
        public async Task<IActionResult> GetDeletedProducts()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _productRepository.GetDeletedProductsAsync();
            return Ok(result);
        }
        [HttpGet("StockProduct")]
        public async Task<IActionResult> GetStockProducts()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _productRepository.GetAllProductsAsync();
            return Ok(result);
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
