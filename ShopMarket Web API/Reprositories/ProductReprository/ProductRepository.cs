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

        public async Task<Product> CreateAsync(ProductCreateDto productModel)
        {
            var product = _mapper.Map<Product>(productModel);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteAsync(int productId)
        {
            var result = await _context.Products.FirstOrDefaultAsync(m => m.Id == productId);
            if (result != null)
            {
                result.IsDeleted = true;
                await _context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<IList<Product>> GetDeletedProductsAsync() =>
            await _context.Products.Where(m => m.IsDeleted == true).ToListAsync();

        public async Task<IList<Product>> GetAllProductsAsync() =>
            await _context.Products.Where(m => m.IsDeleted == false).ToListAsync();

        public async Task<bool> IsProductExist(int productId) =>
             await _context.Products.AnyAsync(m => m.Id == productId && m.IsDeleted == false);

        public async Task<Product> UpdateAsync(int productId, ProductUpdatedDto productModel)
        {
            var result = await _context.Products.FirstOrDefaultAsync(m => m.Id == productId);
            if (result == null)
                return null;

            _mapper.Map(productModel, result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<Product> GetProductByIdAsync(int productId) =>
            await _context.Products.FirstOrDefaultAsync(m => m.Id == productId);

    }
}
