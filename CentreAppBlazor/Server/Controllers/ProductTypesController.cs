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
    public class ProductTypesController : ControllerBase
    {
        private readonly TechContext _context;

        public ProductTypesController(TechContext context)
        {
            _context = context;
        }

        // GET: api/ProductTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductTypes>>> GetProductTypes()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        // GET: api/ProductTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTypes>> GetProductTypes(int id)
        {
            var productTypes = await _context.ProductTypes.FindAsync(id);

            if (productTypes == null)
            {
                return NotFound();
            }

            return productTypes;
        }

        // PUT: api/ProductTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductTypes(int id, ProductTypes productTypes)
        {
            if (id != productTypes.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(productTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductTypesExists(id))
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

        // POST: api/ProductTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductTypes>> PostProductTypes(ProductTypes productTypes)
        {
            if (!ModelState.IsValid) BadRequest();
            _context.ProductTypes.Add(productTypes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductTypes", new { id = productTypes.Id }, productTypes);
        }

        // DELETE: api/ProductTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductTypes>> DeleteProductTypes(int id)
        {
            var productTypes = await _context.ProductTypes.FindAsync(id);
            if (productTypes == null)
            {
                return NotFound();
            }

            _context.ProductTypes.Remove(productTypes);
            await _context.SaveChangesAsync();

            return productTypes;
        }

        private bool ProductTypesExists(int id)
        {
            return _context.ProductTypes.Any(e => e.Id == id);
        }
    }
}
