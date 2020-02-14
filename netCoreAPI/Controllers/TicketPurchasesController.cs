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
    public class TicketPurchasesController : ControllerBase
    {
        private readonly TicketsDBContext _context;

        public TicketPurchasesController(TicketsDBContext context)
        {
            _context = context;
        }

        // GET: api/TicketPurchases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketPurchase>>> GetTicketPurchase()
        {
            return await _context.TicketPurchase.ToListAsync();
        }

        // GET: api/TicketPurchases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketPurchase>> GetTicketPurchase(int id)
        {
            var ticketPurchase = await _context.TicketPurchase.FindAsync(id);

            if (ticketPurchase == null)
            {
                return NotFound();
            }

            return ticketPurchase;
        }

        // PUT: api/TicketPurchases/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicketPurchase(int id, TicketPurchase ticketPurchase)
        {
            if (id != ticketPurchase.PurchaseId)
            {
                return BadRequest();
            }

            _context.Entry(ticketPurchase).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketPurchaseExists(id))
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

        // POST: api/TicketPurchases
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<TicketPurchase>> PostTicketPurchase(TicketPurchase ticketPurchase)
        {
            _context.TicketPurchase.Add(ticketPurchase);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TicketPurchaseExists(ticketPurchase.PurchaseId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTicketPurchase", new { id = ticketPurchase.PurchaseId }, ticketPurchase);
        }

        // DELETE: api/TicketPurchases/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TicketPurchase>> DeleteTicketPurchase(int id)
        {
            var ticketPurchase = await _context.TicketPurchase.FindAsync(id);
            if (ticketPurchase == null)
            {
                return NotFound();
            }

            _context.TicketPurchase.Remove(ticketPurchase);
            await _context.SaveChangesAsync();

            return ticketPurchase;
        }

        private bool TicketPurchaseExists(int id)
        {
            return _context.TicketPurchase.Any(e => e.PurchaseId == id);
        }
    }
}
