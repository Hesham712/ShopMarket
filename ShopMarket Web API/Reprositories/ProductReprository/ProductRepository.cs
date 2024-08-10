using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using ShopMarket_Web_API.Data;
using ShopMarket_Web_API.Dtos.Product;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Reprositories.ProductReprository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProductDetailsDto> CreateAsync(ProductCreateDto productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDetailsDto>(product);
        }

        public async Task<bool> DeleteAsync(int productId)
        {
            var result = await _context.Products.FirstOrDefaultAsync(m => m.Id == productId);
            if (result != null)
            {
                result.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IList<ProductDetailsDto>> GetDeletedProductsAsync() =>
            _mapper.Map<List<ProductDetailsDto>>(await _context.Products.Where(m => m.IsDeleted == false).ToListAsync());

        public async Task<IList<ProductDetailsDto>> GetStockProductsAsync() =>
            _mapper.Map<List<ProductDetailsDto>>(await _context.Products.Where(m => m.IsDeleted == false).ToListAsync());

        public async Task<bool> IsProductExist(int productId) =>
             await _context.Products.AnyAsync(m => m.Id == productId && m.IsDeleted == false);

        public async Task<ProductDetailsDto> UpdateAsync(int productId, ProductUpdatedDto productModel)
        {
            var result = await _context.Products.FirstOrDefaultAsync(m => m.Id == productId);
            if (result == null)
                return null;

            _mapper.Map(productModel, result);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDetailsDto>(result);
        }

        public async Task<ProductDetailsDto> GetProductByIdAsync(int productId) =>
            _mapper.Map<ProductDetailsDto>(await _context.Products.FirstOrDefaultAsync(m => m.Id == productId));

    }
}
