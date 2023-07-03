using MarketApi.Models;
using MarketApi.Models.ModelView;

namespace MarketApi.Services
{
    public interface ISuppliersRepository
    {
        Task<Supplier> createSupplier(SupplierWithoutId newSupplier);
        Task deleteSupplier(int id);
        Task<(List<Supplier>, PaginationMetaData)> getAllSupplierPagination(int pageNumber, int pageSize, string? name);
        Task<List<Supplier>> getAllSuppliers();
        Task<Supplier?> getSupplierById(int id);
        Task<Supplier?> updateSupplier(int id, SupplierWithoutId newSupplier);
        Task<List<Part>> getPartBySupplierId(int id);
    }
}