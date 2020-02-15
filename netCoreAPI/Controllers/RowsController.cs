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
    public class RowsController : ControllerBase
    {
        private readonly TicketsDBContext _context;

        public RowsController(TicketsDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all Rows.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   GET /rows
        ///   {
        ///   "rowId": 1,
        ///    "rowName": "R1",
        ///    "sectionId": 1,
        ///   }
        /// </remarks> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Row>>> GetRow()
        {
            return await _context.Row.ToListAsync();
        }

        /// <summary>
        /// Gets specific row.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   GET /rows/{id}
        ///   {
        ///   "rowId": 1,
        ///    "rowName": "R1",
        ///    "sectionId": 1,
        ///   }
        /// </remarks> 
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<ActionResult<Row>> GetRow(int id)
        {
            var row = await _context.Row.FindAsync(id);

            if (row == null)
            {
                return NotFound();
            }

            return row;
        }

        ///<summary>
        /// Updates specific Row.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   PUT /rows/{id}
        ///   {
        ///   "rowId": 1,
        ///    "rowName": "R1",
        ///    "sectionId": 1,
        ///   }
        /// </remarks> 
        /// <param name="id"></param>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRow(int id, Row row)
        {
            if (id != row.RowId)
            {
                return BadRequest();
            }

            _context.Entry(row).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RowExists(id))
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
        /// Creates a Row.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   POST /row
        ///   {
        ///   "rowId": 1,
        ///    "rowName": "R1",
        ///    "sectionId": 1,
        ///   }
        /// </remarks> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<Row>> PostRow(Row row)
        {
            _context.Row.Add(row);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRow", new { id = row.RowId }, row);
        }

        /// <summary>
        /// Deletes a specific Row.
        /// </summary>
        /// <param name="id"></param>
        /// 	[ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Row>> DeleteRow(int id)
        {
            var row = await _context.Row.FindAsync(id);
            if (row == null)
            {
                return NotFound();
            }

            _context.Row.Remove(row);
            await _context.SaveChangesAsync();

            return row;
        }

        private bool RowExists(int id)
        {
            return _context.Row.Any(e => e.RowId == id);
        }
    }
}
