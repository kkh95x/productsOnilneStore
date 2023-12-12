using AutoMapper;
using Data;
using MarketApi.Models;
using MarketApi.Models.ModelView;
using MarketApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsRepostory1 productRepostory;
        //private readonly IMapper mapper;

        public ProductsController(IProductsRepostory1 carsRepostory)
        {
            this.productRepostory = carsRepostory;
            //this.mapper = mapper;
        }
        //wil return list form  all rows 
        [HttpGet]
        public async Task<ActionResult<List<ProductWithoutId>>> getAllCars()

        {
            var proudcts = await productRepostory.getAllProducts();

            return Ok(proudcts);

        }
        //Endpoint for pagination
        [HttpGet("pagination")]
        public async Task<ActionResult<List<Product>>> getAllCarsPagination([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? model)

        {
            var (cars,pageination) = await productRepostory.getAllProductsPagination(pageNumber, pageSize, model);
            Response.Headers.Append("pageination", pageination.ToString());
            return Ok(cars);

        }

        [HttpPost]
        public async Task<ActionResult<List<Product>>> createProudct([FromBody] ProductWithoutId  product)

        {
            var newProduct = await productRepostory.createProudct(product);
            return Ok(newProduct);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> getCar([FromRoute] int id)

        {
            var product = await productRepostory.getProductsById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string> >deleteProduct([FromRoute] int id)

        {
           await productRepostory.deleteProduct(id);
           
            return Ok($"product have id= {id} has been deleted...");

        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Product>> updateProduct([FromRoute] int id, ProductWithoutId productData )

        {
            var newProduct = await productRepostory.updateProducts(id, productData);
            return Ok(newProduct);

        }
       
    }
}