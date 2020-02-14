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
    [Route("api/[controller]/action")]
    [ApiController]
    public class TicketPurchaseSeatsController : ControllerBase
    {
        private readonly TicketsDBContext _context;

        public TicketPurchaseSeatsController(TicketsDBContext context)
        {
            _context = context;
        }

        // GET: api/TicketPurchaseSeats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketPurchaseSeat>>> GetTicketPurchaseSeat()
        {
            return await _context.TicketPurchaseSeat.ToListAsync();
        }

        // GET: api/TicketPurchaseSeats/5
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

        // PUT: api/TicketPurchaseSeats/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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

        // POST: api/TicketPurchaseSeats
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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

        // DELETE: api/TicketPurchaseSeats/5
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
