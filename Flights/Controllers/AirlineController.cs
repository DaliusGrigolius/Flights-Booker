using Flights.Data;
using Flights.ReadModels;
using Microsoft.AspNetCore.Mvc;

namespace Flights.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
        private readonly Entities _entities;

        public AirlineController(Entities entities)
        {
            _entities = entities;
        }


        [ProducesResponseType(typeof(AirlineDetailsRm), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{airlineName}")]
        public ActionResult<AirlineDetailsRm> Find(string airlineName)
        {
            var airline = _entities.Airlines.SingleOrDefault(a => a.Name == airlineName);

            if (airline == null)
                return NotFound();

            var readModel = new AirlineDetailsRm(
                airline.Name,
                airline.Description,
                airline.Logo
                );

            return Ok(readModel);
        }
    } 
}
