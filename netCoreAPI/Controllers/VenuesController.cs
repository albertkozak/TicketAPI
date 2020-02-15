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
    public class VenuesController : ControllerBase
    {
        private readonly TicketsDBContext _context;

        public VenuesController(TicketsDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all Venues.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   GET /venues
        ///   {
        ///    "venueName": "Kore Stadium",
        ///    "capacity": 100,
        ///   }
        /// </remarks> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Venue>>> GetVenue()
        {
            return await _context.Venue.ToListAsync();
        }

        /// <summary>
        /// Gets specific Venue.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   GET /venues/{id}
        ///   {
        ///    "venueName": "Kore Stadium",
        ///    "capacity": 100,
        ///   }
        /// </remarks> 
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Venue>> GetVenue(string id)
        {
            var venue = await _context.Venue.FindAsync(id);

            if (venue == null)
            {
                return NotFound();
            }

            return venue;
        }

        /// <summary>
        /// Updates specific Venue.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   PUT /venues/{id}
        ///   {
        ///    "venueName": "Kore Stadium",
        ///    "capacity": 101,
        ///   }
        /// </remarks> 
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVenue(string id, Venue venue)
        {
            if (id != venue.VenueName)
            {
                return BadRequest();
            }

            _context.Entry(venue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueExists(id))
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
        /// Creates a Venue.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   POST /venues
        ///   {
        ///    "venueName": "Kore Stadium",
        ///    "capacity": 100,
        ///   }
        /// </remarks> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Venue>> PostVenue(Venue venue)
        {
            _context.Venue.Add(venue);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VenueExists(venue.VenueName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetVenue", new { id = venue.VenueName }, venue);
        }

        /// <summary>
        /// Deletes a specific Venue.
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Venue>> DeleteVenue(string id)
        {
            var venue = await _context.Venue.FindAsync(id);
            if (venue == null)
            {
                return NotFound();
            }

            _context.Venue.Remove(venue);
            await _context.SaveChangesAsync();

            return venue;
        }

        private bool VenueExists(string id)
        {
            return _context.Venue.Any(e => e.VenueName == id);
        }
    }
}
