using MarketApi.Models.ModelView;
using MarketApi.Models;
using MarketApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MarketApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartsController : ControllerBase
    {

        private readonly IPartsRepository partsRepository;
        private readonly IMapper mapper;

        public PartsController(IPartsRepository partsRepository,IMapper mapper)
        {
            this.partsRepository = partsRepository;
            this.mapper = mapper;
        }
        //wil return list form  all rows 
        [HttpGet]
        public async Task<ActionResult<List<PartWitouthCars>>> getAllParts()

        {
            var parts = await partsRepository.getAllParts();
            var partsWitoutCars = mapper.Map<List<PartWitouthCars>>(parts);
            return Ok(partsWitoutCars);

        }
        //Endpoint for pagination
        [HttpGet("pagination")]
        public async Task<ActionResult<List<Part>>> getAllPartsPagination([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] int? price)

        {
            var parts = await partsRepository.getAllPartsPagination(pageNumber, pageSize, price);
            return Ok(parts);

        }

        [HttpPost]
        public async Task<ActionResult<List<Part>>> createCar([FromBody] PartWithoutID part)

        {
            var newPart = await partsRepository.createPart(part);
            return Ok(newPart);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Part>> getPart([FromRoute] int id)

        {
            var part = await partsRepository.getPartById(id);
            if (part == null)
            {
                return NotFound();
            }
            return Ok(part);

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> deletePart([FromRoute] int id)

        {
            await partsRepository.getPartById(id);

            return Ok($"Part have id= {id} has been deleted...");

        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Part>> updatePart([FromRoute] int id, PartWithoutID part)

        {
            var newPart = await partsRepository.updatePart(id, part);
            return Ok(newPart);

        }
    }
}
