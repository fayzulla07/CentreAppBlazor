using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Shared.Domain;
using Microsoft.AspNetCore.Authorization;

namespace CentreAppBlazor.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerTypesController : ControllerBase
    {
        private readonly TechContext _context;

        public CustomerTypesController(TechContext context)
        {
            _context = context;
        }

        // GET: api/CustomerTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerTypes>>> GetCustomerTypes()
        {
            return await _context.CustomerTypes.ToListAsync();
        }

        // GET: api/CustomerTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerTypes>> GetCustomerTypes(int id)
        {
            var customerTypes = await _context.CustomerTypes.FindAsync(id);

            if (customerTypes == null)
            {
                return NotFound();
            }

            return customerTypes;
        }

        // PUT: api/CustomerTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerTypes(int id, CustomerTypes customerTypes)
        {
            if (id != customerTypes.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(customerTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerTypesExists(id))
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

        // POST: api/CustomerTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CustomerTypes>> PostCustomerTypes(CustomerTypes customerTypes)
        {
            if (!ModelState.IsValid) BadRequest();
            _context.CustomerTypes.Add(customerTypes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerTypes", new { id = customerTypes.Id }, customerTypes);
        }

        // DELETE: api/CustomerTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CustomerTypes>> DeleteCustomerTypes(int id)
        {
            var customerTypes = await _context.CustomerTypes.FindAsync(id);
            if (customerTypes == null)
            {
                return NotFound();
            }

            _context.CustomerTypes.Remove(customerTypes);
            await _context.SaveChangesAsync();

            return customerTypes;
        }

        private bool CustomerTypesExists(int id)
        {
            return _context.CustomerTypes.Any(e => e.Id == id);
        }
    }
}
