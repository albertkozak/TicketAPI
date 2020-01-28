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
    [Route("api/[controller]")]
    [ApiController]
    public class RowsController : ControllerBase
    {
        private readonly TicketsDBContext _context;

        public RowsController(TicketsDBContext context)
        {
            _context = context;
        }

        // GET: api/Rows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Row>>> GetRow()
        {
            return await _context.Row.ToListAsync();
        }

        // GET: api/Rows/5
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

        // PUT: api/Rows/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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

        // POST: api/Rows
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Row>> PostRow(Row row)
        {
            _context.Row.Add(row);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRow", new { id = row.RowId }, row);
        }

        // DELETE: api/Rows/5
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
