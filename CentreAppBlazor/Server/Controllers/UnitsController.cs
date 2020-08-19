using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using CentreAppBlazor.Shared.Dto;

namespace CentreAppBlazor.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly TechContext _context;

        public UnitsController(TechContext context)
        {
            _context = context;
        }

        // GET: api/Units
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Units>>> GetUnits()
        {
            return await _context.Units.ToListAsync();
        }

        // GET: api/Units/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Units>> GetUnits(int id)
        {
            var units = await _context.Units.FindAsync(id);

            if (units == null)
            {
                return NotFound();
            }

            return units;
        }

        // PUT: api/Units/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnits(int id, Units units)
        {
            if (units.Id != id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(units).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnitsExists(units.Id))
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

        // POST: api/Units
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Units>> PostUnits(Units units)
        {
            if (!ModelState.IsValid) BadRequest();
            _context.Units.Add(units);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnits", new { id = units.Id }, units);
        }

        // DELETE: api/Units/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Units>> DeleteUnits(int id)
        {
            var units = await _context.Units.FindAsync(id);
            if (units == null)
            {
                return NotFound();
            }

            _context.Units.Remove(units);
            await _context.SaveChangesAsync();

            return units;
        }

        private bool UnitsExists(int id)
        {
            return _context.Units.Any(e => e.Id == id);
        }
    }
}
