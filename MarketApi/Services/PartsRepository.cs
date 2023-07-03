using Data;
using MarketApi.Models;
using MarketApi.Models.ModelView;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketApi.Services
{
    public class PartsRepository : IPartsRepository
    {
        private readonly MyDbContext myDb;

        public PartsRepository(MyDbContext myDb)
        {
            this.myDb = myDb;
        }


        public async Task deletePart(int id)
        {
            var part = await myDb.Parts.FindAsync(id);

            if (part != null)
            {
                myDb.Parts.Remove(part);
                await myDb.SaveChangesAsync();
            }


        }
        public async Task<List<Part>> getAllParts()
        {

            return await myDb.Parts.Include(p=>p.supplierModel).Include(p=>p.Cars)
                //.OrderBy((c=>c.Year)

                .ToListAsync();



        }
        public async Task<(List<Part>, PaginationMetaData)> getAllPartsPagination(int pageNumber, int pageSize, int? price)
        {
            var totalItemCount = await myDb.Parts.CountAsync();
            var paginationMetaData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            var query = myDb.Parts as IQueryable<Part>;
            if (price!=null &&price>=0)
            {
                query = query.Where(c => c.Price == price);
            }
            var result = await query.Include(p => p.supplierModel).Include(p => p.Cars)
                //.OrderBy((c=>c.Year)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (result, paginationMetaData);

        }

        public async Task<Part?> getPartById(int id)
        {

            return await myDb.Parts.Include(p => p.supplierModel).Include(p => p.Cars).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Part> createPart(PartWithoutID newPart)
        {
            var part = new Part() { Name = newPart.Name, Price = newPart.Price, Quntity = newPart.Quntity,SupplierId=newPart.SupplierId };


            myDb.Parts.Add(part);

            await myDb.SaveChangesAsync();
            part = myDb.Parts.Include(p => p.supplierModel).Include(p => p.Cars).FirstOrDefault(p => p == part);
            return part;

        }

        public async Task<Part?> updatePart(int id, PartWithoutID partData)
        {
            var part = await myDb.Parts.FirstOrDefaultAsync(c => c.Id == id);

            if (part != null)
            {
                part.Price = partData.Price;
                part.Quntity = partData.Quntity;
                part.Name = partData.Name;
                await myDb.SaveChangesAsync();
                return part;
            }

            return null;
        }


    }
}
