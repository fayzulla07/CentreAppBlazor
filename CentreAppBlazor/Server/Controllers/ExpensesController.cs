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
    public class ExpensesController : ControllerBase
    {
        private readonly TechContext _context;

        public ExpensesController(TechContext context)
        {
            _context = context;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expenses>>> GetExpenses()
        {
            return await _context.Expenses.ToListAsync();
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expenses>> GetExpenses(int id)
        {
            var Expenses = await _context.Expenses.FindAsync(id);

            if (Expenses == null)
            {
                return NotFound();
            }

            return Expenses;
        }

        // PUT: api/Expenses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenses(int id, Expenses Expenses)
        {
            if (Expenses.Id != id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(Expenses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpensesExists(Expenses.Id))
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

        // POST: api/Expenses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Expenses>> PostExpenses(Expenses Expenses)
        {
            if (!ModelState.IsValid) BadRequest();
            _context.Expenses.Add(Expenses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpenses", new { id = Expenses.Id }, Expenses);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Expenses>> DeleteExpenses(int id)
        {
            var Expenses = await _context.Expenses.FindAsync(id);
            if (Expenses == null)
            {
                return NotFound();
            }

            _context.Expenses.Remove(Expenses);
            await _context.SaveChangesAsync();

            return Expenses;
        }

        private bool ExpensesExists(int id)
        {
            return _context.Expenses.Any(e => e.Id == id);
        }
    }
}
