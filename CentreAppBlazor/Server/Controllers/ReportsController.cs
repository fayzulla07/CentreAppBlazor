using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentreAppBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly TechContext _context;

        public ReportsController(TechContext context)
        {
            _context = context;
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reports>>> GetReports()
        {
            return await _context.Reports.ToListAsync();
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reports>> GetReports(int id)
        {
            var Reports = await _context.Reports.FindAsync(id);

            if (Reports == null)
            {
                return NotFound();
            }

            return Reports;
        }

        // PUT: api/Reports/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReports(int id, Reports Reports)
        {
            if (Reports.Id != id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(Reports).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportsExists(Reports.Id))
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

        // POST: api/Reports
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Reports>> PostReports(Reports Reports)
        {
            if (!ModelState.IsValid) BadRequest();
            _context.Reports.Add(Reports);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReports", new { id = Reports.Id }, Reports);
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reports>> DeleteReports(int id)
        {
            var Reports = await _context.Reports.FindAsync(id);
            if (Reports == null)
            {
                return NotFound();
            }

            _context.Reports.Remove(Reports);
            await _context.SaveChangesAsync();

            return Reports;
        }

        private bool ReportsExists(int id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }
    }
}
