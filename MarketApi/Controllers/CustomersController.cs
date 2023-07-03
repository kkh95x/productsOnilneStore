using MarketApi.Models.ModelView;
using MarketApi.Models;
using MarketApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerRepostory customerRepostory;

        public CustomersController(ICustomerRepostory customerRepostory)
        {
            this.customerRepostory = customerRepostory;
        }
        //wil return list form  all rows 
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> getAllCustomers()

        {
            var customers = await customerRepostory.getAllCustomers();
            return Ok(customers);

        }
        //Endpoint for pagination
        [HttpGet("pagination")]
        public async Task<ActionResult<List<Customer>>> getAllCstomersPagination([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? name)

        {
            var customers = await customerRepostory.getAllCustomersPagination(pageNumber, pageSize, name);
            return Ok(  customers);

        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> createCustomer([FromBody] CustomerWithoutId customer)

        {
            var newCustomer = await customerRepostory.createCustomer(customer);
            return Ok(newCustomer);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> getCustomer([FromRoute] int id)

        {
            var customer = await customerRepostory.getCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> deleteCustomer([FromRoute] int id)

        {
            await customerRepostory.deleteCustomer(id);

            return Ok($"Customer have id= {id} has been deleted...");

        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Customer>> updateCar([FromRoute] int id, CustomerWithoutId customer)

        {
            var newCustomer = await customerRepostory.updateCustomer(id, customer);
            return Ok(newCustomer);

        }
    }
}
