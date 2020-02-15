using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netCoreAPI.Models.TicketAPI;

namespace netCoreAPI.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]
    public class EventSeatsController : ControllerBase
    {
        private readonly TicketsDBContext _context;

        public EventSeatsController(TicketsDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all Event Seats.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   GET /eventseats
        ///   {
        ///    "eventSeatId": 50,
        ///    "seatId": 50,
        ///    "eventId": 1,
        ///    "eventSeatPrice": 20.00
        ///   }
        /// </remarks> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventSeat>>> GetEventSeat()
        {
            return await _context.EventSeat.ToListAsync();
        }

        /// <summary>
        /// Gets specific Event Seat.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   GET /eventseats/{id}
        ///   {
        ///    "eventSeatId": 50,
        ///    "seatId": 50,
        ///    "eventId": 1,
        ///    "eventSeatPrice": 20.00
        ///   }
        /// </remarks> 
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<EventSeat>> GetEventSeat(int id)
        {
            var eventSeat = await _context.EventSeat.FindAsync(id);

            if (eventSeat == null)
            {
                return NotFound();
            }

            return eventSeat;
        }

        /// <summary>
        /// Updates specific Event Seat.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   PUT /eventseats/{id}
        ///   {
        ///    "eventSeatId": 50,
        ///    "seatId": 50,
        ///    "eventId": 1,
        ///    "eventSeatPrice": 50.00
        ///   }
        /// </remarks> 
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventSeat(int id, EventSeat eventSeat)
        {
            if (id != eventSeat.EventSeatId)
            {
                return BadRequest();
            }

            _context.Entry(eventSeat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventSeatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates an Event Seat.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   POST /eventseats
        ///   {
        ///    "eventSeatId": 50,
        ///    "seatId": 50,
        ///    "eventId": 1,
        ///    "eventSeatPrice": 50.00
        ///   }
        /// </remarks> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<EventSeat>> PostEventSeat(EventSeat eventSeat)
        {
            _context.EventSeat.Add(eventSeat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEventSeat", new { id = eventSeat.EventSeatId }, eventSeat);
        }

        /// <summary>
        /// Deletes a specific EventSeat.
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventSeat>> DeleteEventSeat(int id)
        {
            var eventSeat = await _context.EventSeat.FindAsync(id);
            if (eventSeat == null)
            {
                return NotFound();
            }

            _context.EventSeat.Remove(eventSeat);
            await _context.SaveChangesAsync();

            return eventSeat;
        }

        private bool EventSeatExists(int id)
        {
            return _context.EventSeat.Any(e => e.EventSeatId == id);
        }
    }
}
