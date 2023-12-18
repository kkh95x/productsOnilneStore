using Data;
using MarketApi.Models;
using MarketApi.Models.ModelView;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MarketApi.Services
{
    public class ProductsRepostory : IProductsRepostory1
    {
        private readonly MyDbContext myDb;

        public ProductsRepostory(MyDbContext myDb)
        {
            this.myDb = myDb;
        }


        public async Task deleteProduct(int id)
        {
            var product = await myDb.Products.FindAsync(id);

            if (product != null)
            {
                myDb.Products.Remove(product);
                await myDb.SaveChangesAsync();
            }


        }
        public async Task<List<Product>> getAllProducts()
        {

            return await myDb.Products

                .ToListAsync();



        }
        public async Task<(List<Product>, PaginationMetaData)> getAllProductsPagination(int pageNumber, int pageSize, string? model)
        {
            var totalItemCount = await myDb.Products.CountAsync();
            var paginationMetaData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            var query = myDb.Products as IQueryable<Product>;
          
            var result = await query
                //.OrderBy((c=>c.Year)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (result, paginationMetaData);

        }

        public async Task<Product?> getProductsById(int id)
        {

            return await myDb.Products.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Product> createProudct(ProductWithoutId productData)
        {
            //create The Proudcts and reyutn iy with id

            var product = new Product() { createdAt = DateTime.Now, description = productData.description, price = productData.price, name = productData.name };


            myDb.Add(product);
            await myDb.SaveChangesAsync();
            return product;

        }

        public async Task<Product?> updateProducts(int id, ProductWithoutId productData)
        {
            var product = await myDb.Products.FirstOrDefaultAsync(c => c.Id == id);

            if (product != null)
            {
                product.description = productData.description;
                product.name = productData.name;
                product.price = productData.price;
                product.photoUrl = productData.photoUrl;


                await myDb.SaveChangesAsync();
                return product;
            }

            return null;
        }



    }
}
