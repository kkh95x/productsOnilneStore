using MarketApi.Models;
using MarketApi.Models.ModelView;

namespace MarketApi.Services
{
    public interface ISalesRepository
    {
        Task<(Sale?, string?)> createSale(SaleWithoutId saleWithoutId);
        Task deleteSeles(int id);
        Task<List<Sale>> getAllSales();
        Task<(List<Sale>, PaginationMetaData)> getAllSalesPagination(int pageNumber, int pageSize, double? total);
        Task<Sale?> getSaleById(int id);
        Task<Sale?> updateSale(int id, SaleWithoutId saleWithoutId);
    }
}