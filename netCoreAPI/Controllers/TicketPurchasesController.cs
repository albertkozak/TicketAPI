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
    public class TicketPurchasesController : ControllerBase
    {
        private readonly TicketsDBContext _context;

        public TicketPurchasesController(TicketsDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all Ticket Purchases.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   GET /ticketpurchases
        ///   {
        ///    "purchaseId": 1,
        ///    "paymentMethod": "credit",
        ///    "paymentAmount": 120.00,
        ///    "confirmationCode": 001
        ///   }
        /// </remarks> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketPurchase>>> GetTicketPurchase()
        {
            return await _context.TicketPurchase.ToListAsync();
        }

        /// <summary>
        /// Gets specific Ticket Purchase.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   GET /tcketpurchases/{id}
        ///   {
        ///    "purchaseId": 1,
        ///    "paymentMethod": "credit",
        ///    "paymentAmount": 120.00,
        ///    "confirmationCode": 001
        ///   }
        /// </remarks> 
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Updates specific TicketPurchases.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   PUT /ticketpurchases/{id}
        ///   {
        ///    "purchaseId": 1,
        ///    "paymentMethod": "credit",
        ///    "paymentAmount": 120.00,
        ///    "confirmationCode": 001
        ///   }
        /// </remarks> 
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Creates a Ticket Purchase.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   POST /ticketpurchases
        ///   {
        ///    "purchaseId": 1,
        ///    "paymentMethod": "credit",
        ///    "paymentAmount": 120.00,
        ///    "confirmationCode": 001
        ///   }
        /// </remarks> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Deletes a specific Ticket Purchase.
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

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
