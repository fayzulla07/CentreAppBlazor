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
using Microsoft.Net.Http.Headers;
using System.IO;
namespace CentreAppBlazor.Server.Controllers
{
    [Authorize(Roles="admin, manager")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductIncomsController : ControllerBase
    {
        private readonly DapperContext _dappercontext;
        private readonly TechContext _context;

        public ProductIncomsController(DapperContext dappercontext, TechContext context)
        {
            _dappercontext = dappercontext;
            _context = context;
        }

        // GET: api/ProductIncoms
        [HttpGet]
        public async Task<ActionResult<ResponseMessage<IEnumerable<ProductIncoms>>>> GetProductIncoms([FromQuery] ApiQuery query)
        {
            if (query == null)
                return BadRequest();
            var result = _context.ProductIncoms.OrderByDescending(x => x.RegDt).AsQueryable();
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
            result = result.Select(x => new ProductIncoms
            {
                Id = x.Id,
                Amount = x.Amount,
                RegDt = x.RegDt,
                SaleCost = x.SaleCost, 
                UserId = x.UserId,
                SupplierId = x.SupplierId,
                Kurs = x.Kurs,
                ProductionDt = x.ProductionDt,
                Comments = x.Comments,
                OptCost = x.OptCost,
                IncomeCost = x.IncomeCost,
                ProductId = x.ProductId,
                Product = new Products { Id = x.Id, Name = x.Product.Name},
                IncomeNumber = x.IncomeNumber
            });
            return new ResponseMessage<IEnumerable<ProductIncoms>>() { entity = await result.ToListAsync(), TCount = _context.ProductIncoms.Count() };
        }

        // GET: api/ProductIncoms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductIncoms>> GetProductIncoms(int id)
        {
            var productIncoms = await _context.ProductIncoms.FindAsync(id);

            if (productIncoms == null)
            {
                return NotFound();
            }

            return productIncoms;
        }


        // PUT: api/ProductIncoms/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductIncoms(int id, ProductIncoms productIncoms)
        {
            if (id != productIncoms.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }
            _context.Entry(productIncoms).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductIncomsExists(id))
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

        // POST: api/ProductIncoms
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProductIncoms>> PostProductIncoms(ProductIncoms productIncoms)
        {
            if (!ModelState.IsValid) BadRequest();
            _context.ProductIncoms.Add(productIncoms);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductIncoms", new { id = productIncoms.Id }, productIncoms);
        }
        [AllowAnonymous]
        [HttpGet("GetInvoice/{Id}")]
        public async Task<FileStreamResult> GetInvoice(int Id)
        {
            var productIncoms = await _context.ProductIncoms.Include(x => x.Product).ThenInclude(x => x.Unit).Where(p => p.IncomeNumber == Id).ToListAsync();
            var supplierName = _context.Suppliers.Where(x => x.Id == productIncoms.FirstOrDefault().SupplierId).Select(p => p.Name).FirstOrDefault();
            Services.Print print = new Services.Print();
            var excel = print.GetIncomeFile(productIncoms, supplierName);
            Stream stream = new MemoryStream(excel);
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "IncomeInvoice.xlsx"
            };
        }

        // DELETE: api/ProductIncoms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductIncoms>> DeleteProductIncoms(int id)
        {
            var productIncoms = await _context.ProductIncoms.FindAsync(id);
            if (productIncoms == null)
            {
                return NotFound();
            }

            await _dappercontext.ExecuteAsync("SP_DeleteProductIncome", new { Id = id }, CommandType: System.Data.CommandType.StoredProcedure);

            return productIncoms;
        }

        private bool ProductIncomsExists(int id)
        {
            return _context.ProductIncoms.Any(e => e.Id == id);
        }

        
    }
}
