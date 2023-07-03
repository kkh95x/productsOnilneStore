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
    public class CustomerRepostory : ICustomerRepostory
    {
        private readonly MyDbContext myDb;

        public CustomerRepostory(MyDbContext myDb)
        {
            this.myDb = myDb;
        }

        public async Task deleteCustomer(int id)
        {
            var car = await myDb.Customers.FindAsync(id);

            if (car != null)
            {
                myDb.Customers.Remove(car);
                await myDb.SaveChangesAsync();
            }


        }
        public async Task<List<Customer>> getAllCustomers()
        {

            return await myDb.Customers
                //.OrderBy((c=>c.Year)

                .ToListAsync();



        }
        public async Task<(List<Customer>, PaginationMetaData)> getAllCustomersPagination(int pageNumber, int pageSize, string? name)
        {
            var totalItemCount = await myDb.Customers.CountAsync();
            var paginationMetaData = new PaginationMetaData(totalItemCount, pageSize, pageNumber);
            var query = myDb.Customers as IQueryable<Customer>;
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

        public async Task<Customer?> getCustomerById(int id)
        {

            return await myDb.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Customer> createCustomer(CustomerWithoutId newCustomer)
        {
            var customer = new Customer() { Age = newCustomer.Age, Address = newCustomer.Address, Name = newCustomer.Name };


            myDb.Customers.Add(customer);

            await myDb.SaveChangesAsync();
            return customer;

        }

        public async Task<Customer?> updateCustomer(int id, CustomerWithoutId customerData)
        {
            var customer = await myDb.Customers.FirstOrDefaultAsync(c => c.Id == id);

            if (customer != null)
            {
                customer.Age = customerData.Age;
                customer.Address = customerData.Address;
                customerData.Name = customerData.Name;

                myDb.Customers.Update(customer);
                await myDb.SaveChangesAsync();
                return customer;
            }

            return null;
        }


    }
}
