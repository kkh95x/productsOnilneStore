using MarketApi.Models.ModelView;
using MarketApi.Models;
using MarketApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesRepository salesRepository;

        public SalesController(ISalesRepository salesRepository)
        {
            this.salesRepository = salesRepository;
        }
        //wil return list form  all rows 
        [HttpGet]
        public async Task<ActionResult<List<Sale>>> getAllSales()

        {
            var sales = await salesRepository.getAllSales();
            return Ok(sales);

        }
        //Endpoint for pagination
        [HttpGet("pagination")]
        public async Task<ActionResult<List<Sale>>> getAllSalesPagination([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] double? total)

        {
            var (sales,pagination) = await salesRepository.getAllSalesPagination(pageNumber, pageSize, total);
            Response.Headers.Add("pagination", pagination.ToString());
            return Ok(sales);

        }

        [HttpPost]
        public async Task<ActionResult<List<Sale>>> createSale([FromBody] SaleWithoutId sale)

        {
            var (newSale,message) = await salesRepository.createSale(sale);
            if(newSale == null)
            {
                return NotFound(message);

            }
            return Ok(newSale);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> getSale([FromRoute] int id)

        {
            var sale = await salesRepository.getSaleById(id);
            if (sale == null)
            {
                return NotFound();
            }
            return Ok(sale);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> deleteSale([FromRoute] int id)

        {
            await salesRepository.deleteSeles(id);

            return Ok($"sale have id= {id} has been deleted...");

        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Sale>> updateSale([FromRoute] int id, SaleWithoutId sale)

        {
            var newSale = await salesRepository.updateSale(id, sale);
            return Ok(newSale);

        }
    }
}
