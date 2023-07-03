using MarketApi.Models;
using MarketApi.Models.ModelView;

namespace MarketApi.Services
{
    public interface ICarsRepostory
    {
        Task<Car> createCar(CarWithoutId newCar);
        Task deleteCar(int id);
        Task<List<Car>> getAllCars();
        Task<(List<Car>, PaginationMetaData)> getAllCarsPagination(int pageNumber, int pageSize, string? model);
        Task<Car?> getCarById(int id);
        Task<Car?> updateCar(int id, CarWithoutId carData);
        Task<List<Part>?> getPartsByIdCar(int Id);
        Task<(List<Part>?, string? error)> addPartsToCar(int Id,int partId);

    }
}