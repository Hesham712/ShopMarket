using ShopMarket_Web_API.Dtos.Product;
using ShopMarket_Web_API.Models;

namespace ShopMarket_Web_API.Reprositories.ProductReprository
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(ProductCreateDto productModel);
        Task<Product> UpdateAsync(int productId, ProductUpdatedDto productModel);
        Task<IList<Product>> GetAllProductsAsync();
        Task<IList<Product>> GetDeletedProductsAsync();
        Task<Product> GetProductByIdAsync(int productId);
        Task<Product> DeleteAsync(int productId);
        Task<bool> IsProductExist(int productId);

    }
}
