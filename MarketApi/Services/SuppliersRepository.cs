
using Data;
using MarketApi.Models;
using MarketApi.Models.ModelView;
using Microsoft.EntityFrameworkCore;

namespace MarketApi.Services
{
    public class SuppliersRepository : ISuppliersRepository
    {
        private readonly MyDbContext myDb;

        public SuppliersRepository(MyDbContext myDb)
        {
            this.myDb = myDb;
        }


        public async Task deleteSupplier(int id)
        {

            var supplier = await myDb.Suppliers.FindAsync(id);

            if (supplier != null)
            {
                myDb.Suppliers.Remove(supplier);
                await myDb.SaveChangesAsync();
            }


        }
        public async Task<List<Supplier>> getAllSuppliers()
        {

            return await myDb.Suppliers
                //.OrderBy((c=>c.Year)

                .ToListAsync();



        }
        public async Task<(List<Supplier>, PaginationMetaData)> getAllSupplierPagination(int pageNumber, int pageSize, string? name)
        {
            var totalItemCount = await myDb.Cars.CountAsync();
            var paginationMetaData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            var query = myDb.Suppliers as IQueryable<Supplier>;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.ToLower().Contains(name.ToLower()));
            }
            var result = await query
                //.OrderBy((c=>c.Year)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (result, paginationMetaData);

        }

        public async Task<Supplier?> getSupplierById(int id)
        {

            return await myDb.Suppliers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Supplier> createSupplier(SupplierWithoutId newSupplier)
        {
            var supplier = new Supplier() { Name = newSupplier.Name, Address = newSupplier.Address };


            myDb.Suppliers.Add(supplier);

            await myDb.SaveChangesAsync();
            return supplier;

        }

        public async Task<Supplier?> updateSupplier(int id, SupplierWithoutId newSupplier)
        {
            var supplier = await myDb.Suppliers.FirstOrDefaultAsync(c => c.Id == id);

            if (supplier != null)
            {
                supplier.Address = newSupplier.Address;
                supplier.Name = newSupplier.Name;
                myDb.Suppliers.Update(supplier);
                await myDb.SaveChangesAsync();
                return supplier;
            }

            return null;
        }

        public async Task<List<Part>> getPartBySupplierId(int id)
        {
            var supplier = await myDb.Suppliers.FirstOrDefaultAsync(s => s.Id == id);
            if (supplier == null)
            {
                return new List<Part>();
            }
            var parts = myDb.Parts.Where(p => p.SupplierId == id).ToList();
            return parts;
        }
    }
}
