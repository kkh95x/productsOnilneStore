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
    public class CarsController : ControllerBase
    {
        private readonly ICarsRepostory carsRepostory;
        private readonly IMapper mapper;

        public CarsController(ICarsRepostory carsRepostory,IMapper mapper)
        {
            this.carsRepostory = carsRepostory;
            this.mapper = mapper;
        }
        //wil return list form  all rows 
        [HttpGet]
        public async Task<ActionResult<List<CarWithoutParts>>> getAllCars()

        {
            var cars = await carsRepostory.getAllCars();
            var carsWithoutParts = mapper.Map<List<CarWithoutParts>>(cars);

            return Ok(carsWithoutParts);

        }
        //Endpoint for pagination
        [HttpGet("pagination")]
        public async Task<ActionResult<List<Car>>> getAllCarsPagination([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] string? model)

        {
            var (cars,pageination) = await carsRepostory.getAllCarsPagination(pageNumber, pageSize, model);
            Response.Headers.Add("pageination", pageination.ToString());
            return Ok(cars);

        }

        [HttpPost]
        public async Task<ActionResult<List<Car>>> createCar([FromBody] CarWithoutId car)

        {
            var newcar = await carsRepostory.createCar(car);
            return Ok(newcar);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> getCar([FromRoute] int id)

        {
            var car = await carsRepostory.getCarById(id);
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string> >deleteCar([FromRoute] int id)

        {
           await carsRepostory.deleteCar(id);
           
            return Ok($"car have id= {id} has been deleted...");

        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Car>> updateCar([FromRoute] int id,CarWithoutId car)

        {
            var newcar = await carsRepostory.updateCar(id,car);
            return Ok(newcar);

        }
        [HttpGet("{id}/Parts")]
        public async Task<ActionResult<List<Part>>> getPartsByCarId([FromRoute] int id)

        {
            var parts = await carsRepostory.getPartsByIdCar(id);
            if (parts == null)
            {
                return NotFound();
            }
            var partsWithoutCars = mapper.Map < List < PartWitouthCars >> (parts);
            return Ok(partsWithoutCars);

        }
        [HttpPost("{id}/Parts")]
        public async Task<ActionResult<List<Part>>> addPartsToCar([FromRoute] int id, [FromBody]int partId)

        {
            var (parts,message) = await carsRepostory.addPartsToCar(id,partId);
            if (parts == null)
            {
                return NotFound(message);
            }
            return Ok(parts);

        }
    }
}