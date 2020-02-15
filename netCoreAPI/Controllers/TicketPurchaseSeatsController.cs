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
    public class TicketPurchaseSeatsController : ControllerBase
    {
        private readonly TicketsDBContext _context;

        public TicketPurchaseSeatsController(TicketsDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all Ticket Purchase Seats.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   GET /ticketpurchaseseats
        ///   {
        ///    "purchaseId": 001,
        ///    "eventSeatId": 100,
        ///    "seatSubTotal": 120.00
        ///   }
        /// </remarks> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketPurchaseSeat>>> GetTicketPurchaseSeat()
        {
            return await _context.TicketPurchaseSeat.ToListAsync();
        }

        /// <summary>
        /// Gets specific Ticket Purchase Seat Id.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   GET /ticketpurchaseseat/{id}
        ///   {
        ///    "purchaseId": 001,
        ///    "eventSeatId": 100,
        ///    "seatSubTotal": 120.00
        ///   }
        /// </remarks> 
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketPurchaseSeat>> GetTicketPurchaseSeat(int id)
        {
            var ticketPurchaseSeat = await _context.TicketPurchaseSeat.FindAsync(id);

            if (ticketPurchaseSeat == null)
            {
                return NotFound();
            }

            return ticketPurchaseSeat;
        }

        /// <summary>
        /// Updates specific Ticket Purchase Seat.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   PUT /ticketpurchaseseat/{id}
        ///   {
        ///    "purchaseId": 001,
        ///    "eventSeatId": 101,
        ///    "seatSubTotal": 120.00
        ///   }
        /// </remarks> 
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketPurchaseSeat(int id, TicketPurchaseSeat ticketPurchaseSeat)
        {
            if (id != ticketPurchaseSeat.EventSeatId)
            {
                return BadRequest();
            }

            _context.Entry(ticketPurchaseSeat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketPurchaseSeatExists(id))
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
        /// Creates a Ticket Purchase Seat.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   POST /ticketpurchaseseat
        ///   {
        ///    "purchaseId": 001,
        ///    "eventSeatId": 101,
        ///    "seatSubTotal": 120.00
        ///   }
        /// </remarks> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<TicketPurchaseSeat>> PostTicketPurchaseSeat(TicketPurchaseSeat ticketPurchaseSeat)
        {
            _context.TicketPurchaseSeat.Add(ticketPurchaseSeat);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TicketPurchaseSeatExists(ticketPurchaseSeat.EventSeatId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTicketPurchaseSeat", new { id = ticketPurchaseSeat.EventSeatId }, ticketPurchaseSeat);
        }

        /// <summary>
        /// Deletes a specific Ticket Purchase Seat.
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<TicketPurchaseSeat>> DeleteTicketPurchaseSeat(int id)
        {
            var ticketPurchaseSeat = await _context.TicketPurchaseSeat.FindAsync(id);
            if (ticketPurchaseSeat == null)
            {
                return NotFound();
            }

            _context.TicketPurchaseSeat.Remove(ticketPurchaseSeat);
            await _context.SaveChangesAsync();

            return ticketPurchaseSeat;
        }

        private bool TicketPurchaseSeatExists(int id)
        {
            return _context.TicketPurchaseSeat.Any(e => e.EventSeatId == id);
        }
    }
}
