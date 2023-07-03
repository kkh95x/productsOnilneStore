using MarketApi.Models.ModelView;
using MarketApi.Models;
using MarketApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
       
            private readonly ISuppliersRepository suppliersRepository;

            public SuppliersController(ISuppliersRepository suppliersRepository)
            {
                this.suppliersRepository = suppliersRepository;
            }
            //wil return list form  all rows 
            [HttpGet]
            public async Task<ActionResult<List<Supplier>>> getAllSuppliers()

            {
                var suppliers = await suppliersRepository.getAllSuppliers();
                return Ok(suppliers);

            }
            //Endpoint for pagination
            [HttpGet("pagination")]
            public async Task<ActionResult<List<Supplier>>> getAllSuppliersPagination([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? name)

            {
                var suppliers = await suppliersRepository.getAllSupplierPagination(pageNumber, pageSize, name);
                return Ok(suppliers);

            }

            [HttpPost]
            public async Task<ActionResult<List<Supplier>>> createCar([FromBody] SupplierWithoutId supplierWithout)

            {
                var newcar = await suppliersRepository.createSupplier(supplierWithout);
                return Ok(newcar);

            }

            [HttpGet("{id}")]
            public async Task<ActionResult<Supplier?>> getSupplierById([FromRoute] int id)

            {
                var suppliers = await suppliersRepository.getSupplierById(id);
                if (suppliers == null)
                {
                    return NotFound();
                }
                return Ok(suppliers);

            }
            [HttpDelete("{id}")]
            public async Task<ActionResult<string>> deleteSupliers([FromRoute] int id)

            {
                await  suppliersRepository.deleteSupplier(id);

                return Ok($"Suppliers have id= {id} has been deleted...");

            }
            [HttpPut("update/{id}")]
            public async Task<ActionResult<Supplier>> updateSupplier([FromRoute] int id, SupplierWithoutId suppliers)

            {
                var newSupplier = await suppliersRepository.updateSupplier(id, suppliers);
                return Ok(newSupplier);

            }
        [HttpGet("{id}/Parts")]
        public async Task<ActionResult< List<Part>>> getPartBySupplierId([FromRoute] int id){
        var parts=await suppliersRepository.getPartBySupplierId(id);
            return Ok(parts);
        }
        }
    
}
