using Flights.ReadModels;
using Flights.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Flights.Dtos;
using Flights.Domain.Errors;
using Flights.Data;
using Microsoft.EntityFrameworkCore;

namespace Flights.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class FlightController : ControllerBase
    {
        private readonly Entities _entities;

        public FlightController(Entities entities)
        {
            _entities = entities;
        }

        [ProducesResponseType(typeof(IEnumerable<FlightRm>), 200)]
        [HttpGet]
        public IEnumerable<FlightRm> Search([FromQuery] FlightSearchParameters @params)
        {
            IQueryable<Flight> flights = _entities.Flights;
            if (!string.IsNullOrWhiteSpace(@params.Destination))
                flights = flights.Where(f => f.Arrival.Place.Contains(@params.Destination));
            
            if (!string.IsNullOrWhiteSpace(@params.From))
                flights = flights.Where(f => f.Departure.Place.Contains(@params.From));

            if (@params.FromDate != null)
                flights = flights.Where(f => f.Departure.Time >= @params.FromDate.Value.Date);

            if (@params.ToDate != null)
                flights = flights.Where(f => f.Departure.Time >= @params.ToDate.Value.Date.AddDays(1).AddTicks(-1));

            if (!string.IsNullOrWhiteSpace(@params.Airline))
            {
                flights = flights.Where(f => f.Airline.Contains(@params.Airline));
            }

            if (@params.NumberOfPassengers != 0 && @params.NumberOfPassengers != null)
                flights = flights.Where(f => f.RemainingNumberOfSeats >= @params.NumberOfPassengers);
            else
                flights = flights.Where(f => f.RemainingNumberOfSeats >= 1);


            var flightRmList = flights
                .Select(flight => new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                flight.RemainingNumberOfSeats
                ));

            return flightRmList;
        }

        [ProducesResponseType(typeof(FlightRm), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public ActionResult<FlightRm> Find(Guid id)
        {
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == id);

            if (flight == null)
                return NotFound();

            var readModel = new FlightRm(
                flight.Id,
                flight.Airline,
                flight.Price,
                new TimePlaceRm(flight.Departure.Place.ToString(), flight.Departure.Time),
                new TimePlaceRm(flight.Arrival.Place.ToString(), flight.Arrival.Time),
                flight.RemainingNumberOfSeats
                );

            return Ok(readModel);
        }

        [ProducesResponseType(typeof(FlightRm), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult Book(BookDto dto)
        {
            var flight = _entities.Flights.SingleOrDefault(f => f.Id == dto.FlightId);

            if (flight == null)
                return NotFound();

            var error = flight.MakeBooking(dto.PassengerEmail, dto.NumberOfSeats);

            if (error is OverbookError)
                return Conflict(new { message = "Not enough seats." });

            try
            {
                _entities.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "An error occured while booking. Please try again." });
            }

            return CreatedAtAction(nameof(Find), new { id = dto.FlightId });
        }
    }
}