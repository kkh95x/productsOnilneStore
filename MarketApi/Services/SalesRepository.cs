using Data;
using MarketApi.Models;
using MarketApi.Models.ModelView;
using Microsoft.EntityFrameworkCore;


namespace MarketApi.Services
{
    public class SalesRepository : ISalesRepository

    {
        private readonly MyDbContext myDb;

        public SalesRepository(MyDbContext myDb)
        {
            this.myDb = myDb;
        }


        public async Task deleteSeles(int id)
        {
            var sale = await myDb.Sales.FindAsync(id);

            if (sale != null)
            {
                myDb.Sales.Remove(sale);
                await myDb.SaveChangesAsync();
            }


        }
        public async Task<List<Sale>> getAllSales()
        {

            return await myDb.Sales.Include(c => c.Customer).Include(c => c.Car)
                //.OrderBy((c=>c.Year)

                .ToListAsync();



        }
        public async Task<(List<Sale>, PaginationMetaData)> getAllSalesPagination(int pageNumber, int pageSize, double? total)
        {
            var totalItemCount = await myDb.Sales.CountAsync();
            var paginationMetaData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            var query = myDb.Sales as IQueryable<Sale>;
            if (total != null && total > 0)
            {
                query = query.Where(c => c.Total == total);
            }
            var result = await query.Include(c => c.Customer).Include(c => c.Car)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (result, paginationMetaData);

        }

        public async Task<Sale?> getSaleById(int id)
        {

            return await myDb.Sales.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<(Sale?,string?)> createSale(SaleWithoutId saleWithoutId)
        {
            var customer = await myDb.Customers.FirstOrDefaultAsync(c => c.Id == saleWithoutId.CostimerId);
            var car = await myDb.Cars.FirstOrDefaultAsync(c => c.Id == saleWithoutId.CarId);
            
            if(customer == null)
            {
                return (null, $"The Customer with id ={saleWithoutId.CostimerId} not Found");
            }
            if (car == null)
            {
                return (null, $"The car with id ={saleWithoutId.CarId} not Found");

            }

            var sale = new Sale() {Car=car,CarId=car.Id,CostimerId=customer.Id, Total = saleWithoutId.Total,Customer=customer};


            myDb.Sales.Add(sale);

            await myDb.SaveChangesAsync();
            return (sale,null);

        }

        public async Task<Sale?> updateSale(int id, SaleWithoutId saleWithoutId)
        {
            var sale = await myDb.Sales.FirstOrDefaultAsync(c => c.Id == id);

            if (sale != null)
            {
                sale.CostimerId = saleWithoutId.CostimerId;
                sale.Total = saleWithoutId.Total;
                sale.CarId = saleWithoutId.CarId;
                myDb.Sales.Update(sale);
                await myDb.SaveChangesAsync();
                return sale;
            }

            return null;
        }


    }
}
