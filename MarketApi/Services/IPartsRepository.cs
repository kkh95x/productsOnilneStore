using MarketApi.Models;
using MarketApi.Models.ModelView;

namespace MarketApi.Services
{
    public interface IPartsRepository
    {
        Task<Part> createPart(PartWithoutID newPart);
        Task deletePart(int id);
        Task<List<Part>> getAllParts();
        Task<(List<Part>, PaginationMetaData)> getAllPartsPagination(int pageNumber, int pageSize, int? price);
        Task<Part?> getPartById(int id);
        Task<Part?> updatePart(int id, PartWithoutID partData);
    }
}