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
    public class SectionsController : ControllerBase
    {
        private readonly TicketsDBContext _context;

        public SectionsController(TicketsDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all Sections.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   GET /sections
        ///   {
        ///   "sectionId": 1,
        ///    "sectionName": "A",
        ///    "venueName": "Kore Stadium"
        ///   }
        /// </remarks> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Section>>> GetSection()
        {
            return await _context.Section.ToListAsync();
        }

        /// <summary>
        /// Gets specific Section.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   GET /sections/{id}
        ///   {
        ///   "sectionId": 1,
        ///    "sectionName": "A",
        ///    "venueName": "Kore Stadium"
        ///   }
        /// </remarks> 
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Section>> GetSection(int id)
        {
            var section = await _context.Section.FindAsync(id);

            if (section == null)
            {
                return NotFound();
            }

            return section;
        }

        /// <summary>
        /// Updates specific Sections.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   PUT /sections/{id}
        ///   {
        ///   "sectionId": 1,
        ///    "sectionName": "A1",
        ///    "venueName": "Kore Stadium"
        ///   }
        /// </remarks> 
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSection(int id, Section section)
        {
            if (id != section.SectionId)
            {
                return BadRequest();
            }

            _context.Entry(section).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SectionExists(id))
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
        /// Creates a Section.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   POST /sections
        ///   {
        ///   "sectionId": 1,
        ///    "sectionName": "A1",
        ///    "venueName": "Kore Stadium"
        ///   }
        /// </remarks> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Section>> PostSection(Section section)
        {
            _context.Section.Add(section);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSection", new { id = section.SectionId }, section);
        }

        /// <summary>
        /// Deletes a specific Section.
        /// </summary>
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Section>> DeleteSection(int id)
        {
            var section = await _context.Section.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }

            _context.Section.Remove(section);
            await _context.SaveChangesAsync();

            return section;
        }

        private bool SectionExists(int id)
        {
            return _context.Section.Any(e => e.SectionId == id);
        }
    }
}
