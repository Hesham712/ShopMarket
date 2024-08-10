using ShopMarket_Web_API.Dtos.Product;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Reprositories.ProductReprository
{
    public interface IProductRepository
    {
        Task<ProductDetailsDto> CreateAsync(ProductCreateDto productModel);
        Task<ProductDetailsDto> UpdateAsync(int productId, ProductUpdatedDto productModel);
        Task<IList<ProductDetailsDto>> GetStockProductsAsync();
        Task<IList<ProductDetailsDto>> GetDeletedProductsAsync();
        Task<ProductDetailsDto> GetProductByIdAsync(int productId);
        Task<bool> DeleteAsync(int productId);
        Task<bool> IsProductExist(int productId);

    }
}
