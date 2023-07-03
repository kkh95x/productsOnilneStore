using Data;
using MarketApi.Models;
using MarketApi.Models.ModelView;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MarketApi.Services
{
    public class CarsRepostory : ICarsRepostory
    {
        private readonly MyDbContext myDb;

        public CarsRepostory(MyDbContext myDb)
        {
            this.myDb = myDb;
        }


        public async Task deleteCar(int id)
        {
            var car = await myDb.Cars.FindAsync(id);

            if (car != null)
            {
                myDb.Cars.Remove(car);
                await myDb.SaveChangesAsync();
            }


        }
        public async Task<List<Car>> getAllCars()
        {

            return await myDb.Cars
                //.OrderBy((c=>c.Year)

                .ToListAsync();



        }
        public async Task<(List<Car>, PaginationMetaData)> getAllCarsPagination(int pageNumber, int pageSize, string? model)
        {
            var totalItemCount = await myDb.Cars.CountAsync();
            var paginationMetaData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            var query = myDb.Cars as IQueryable<Car>;
            if (!string.IsNullOrEmpty(model))
            {
                query = query.Where(c => c.Model.ToLower().Contains(model.ToLower()));
            }
            var result = await query.Include(c=>c.Parts)
                //.OrderBy((c=>c.Year)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (result, paginationMetaData);

        }

        public async Task<Car?> getCarById(int id)
        {

            return await myDb.Cars.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Car> createCar(CarWithoutId newCar)
        {
            //get The Parts from PartsId Whene User Pass Him by NewCar
            List<Part> parts = myDb.Parts.Where(p => newCar.PartsId.Contains(p.Id)).ToList();

            var car = new Car() { Km = newCar.Km, Model = newCar.Model, Year = newCar.Year, Gear = newCar.Gear,Parts=parts};


            myDb.Add(car);
            await myDb.SaveChangesAsync();
            return car;

        }

        public async Task<Car?>  updateCar(int id, CarWithoutId carData)
        {
            var car = await myDb.Cars.FirstOrDefaultAsync(c=>c.Id==id);

            if (car != null)
            {
                car.Km = carData.Km;
                car.Model = carData.Model;
                car.Year = carData.Year;
                car.Gear = carData.Gear;
                myDb.Cars.Update(car);
                await myDb.SaveChangesAsync();
                return car;
            }

            return null;
        }

        public async Task<List<Part>?> getPartsByIdCar(int Id)
        {
           var car=await myDb.Cars.Include(c=>c.Parts).FirstOrDefaultAsync(c=>c.Id==Id);

            if (car == null)
            {
                return null;
            }
            
            return car.Parts;
        }

        public async Task<(List<Part>?,string? error)> addPartsToCar(int Id, int partId)
        {
            var car = await myDb.Cars.Include(c => c.Parts).FirstOrDefaultAsync(c => c.Id == Id);
            var part= await myDb.Parts.FirstAsync(p =>p.Id==partId);
            //check if the objects is empty
            if(car == null)
            {
                return (null, $"Cars with id {Id} is NotFound");
            }
            if(part == null)
            {
                return (null, $"Part With id {partId} NotFound");
            }
            car?.Parts?.Add(part);
            myDb.Cars.Update(car);
            await myDb.SaveChangesAsync();

            return (car.Parts, null);
        }
    }
}
