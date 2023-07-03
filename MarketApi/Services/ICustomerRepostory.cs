using MarketApi.Models;
using MarketApi.Models.ModelView;

namespace MarketApi.Services
{
    public interface ICustomerRepostory
    {
        Task<Customer> createCustomer(CustomerWithoutId newCustomer);
        Task deleteCustomer(int id);
        Task<List<Customer>> getAllCustomers();
        Task<(List<Customer>, PaginationMetaData)> getAllCustomersPagination(int pageNumber, int pageSize, string? name);
        Task<Customer?> getCustomerById(int id);
        Task<Customer?> updateCustomer(int id, CustomerWithoutId customerData);
    }
}