using MarketApi.Models;
using MarketApi.Models.ModelView;

namespace MarketApi.Services
{
    public interface IProductsRepostory1
    {
        Task<Product> createProudct(ProductWithoutId productData);
        Task deleteProduct(int id);
        Task<List<Product>> getAllProducts();
        Task<(List<Product>, PaginationMetaData)> getAllProductsPagination(int pageNumber, int pageSize, string? model);
        Task<Product?> getProductsById(int id);
        Task<Product?> updateProducts(int id, ProductWithoutId productData);
    }
}