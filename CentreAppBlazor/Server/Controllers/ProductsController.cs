using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Shared.Domain;
using CentreAppBlazor.Shared.Dto;
using Radzen;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;

namespace CentreAppBlazor.Server.Controllers
{
    [Authorize(Roles = "admin, manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly TechContext _context;
        private readonly DapperContext _dappercontext;

        public ProductsController(TechContext context, DapperContext dappercontext)
        {
            _context = context;
            _dappercontext = dappercontext;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<ResponseMessage<IEnumerable<Products>>>> GetProducts([FromQuery]ApiQuery query)
        {
            if (query == null)
                return BadRequest();
            var result = _context.Products.AsQueryable();
            if (!string.IsNullOrEmpty(query.Filter))
            {
                result = result.Where(query.Filter);
            }
            if (!string.IsNullOrEmpty(query.OrderBy))
            {
                result = result.OrderBy(query.OrderBy);
            }
            if (query.Skip.HasValue)
            {
                result = result.Skip(query.Skip.Value);
            }
            if (query.Top.HasValue)
            {
                result = result.Take(query.Top.Value);
            }
            try
            {
                await result.ToListAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
            return new ResponseMessage<IEnumerable<Products>>() { entity = await result.ToListAsync(), TCount = _context.Products.Count() };
        }

        [HttpGet("getproducts")]
        public async Task<ActionResult<List<Products>>> GetProducts()
        {
            return _context.Products.ToList();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);

            if (products == null)
            {
                return NotFound();
            }

            return products;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducts(int id, Products products)
        {
            if (id != products.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(products).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts(Products products)
        {
            if (!ModelState.IsValid) BadRequest();
            _context.Products.Add(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProducts", new { id = products.Id }, products);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> DeleteProducts(int id)
        {
            var products = await _context.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            _context.Products.Remove(products);
            await _context.SaveChangesAsync();

            return products;
        }

        private bool ProductsExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
