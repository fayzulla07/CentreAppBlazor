using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CentreAppBlazor.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DropDownController : ControllerBase
    {
        private readonly TechContext _context;
        private readonly DapperContext _dappercontext;

        public DropDownController(TechContext context, DapperContext dappercontext)
        {
            _context = context;
            _dappercontext = dappercontext;
        }

       
        [HttpGet("ProductTypes")]
        public async Task<ActionResult<IEnumerable<ProductTypes>>> GetProductTypes()
        {
            return await _dappercontext.QueryAsync<ProductTypes>("select Id, Name from ProductTypes");
          //  return await _context.ProductTypes.Select(x=> new ProductTypes { Id = x.Id, Name = x.Name }).AsNoTracking().ToListAsync();
        }

        [HttpGet("Customers")]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomers()
        {
            return await _dappercontext.QueryAsync<Customers>("select Id, Name from Customers");
           // return await _context.Customers.Select(x => new Customers { Id = x.Id, Name = x.Name }).AsNoTracking().ToListAsync();
        }

        [HttpGet("Suppliers")]
        public async Task<ActionResult<IEnumerable<Suppliers>>> GetSuppliers()
        {
            return await _dappercontext.QueryAsync<Suppliers>("select Id, Name from Suppliers");
          //  return await _context.Suppliers.Select(x => new Suppliers { Id = x.Id, Name = x.Name }).AsNoTracking().ToListAsync();
        }

        [HttpGet(@"Products/{ProductTypeId}")]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts(int ProductTypeId)
        {
            return await _dappercontext.QueryAsync<Products>("select Id, [Name], Amount as RemainCount, Limit from Products as p WHERE p.ProductTypeId = @ProductTypeId AND p.RemainCount > 0", new { ProductTypeId });
            // return await _context.Products.Where(x => x.ProductTypeId == ProductTypeId && x.RemainCount > 0).AsNoTracking().ToListAsync();
        }

        [HttpGet(@"ProductsWithNull/{ProductTypeId}")]
        public async Task<ActionResult<IEnumerable<Products>>> ProductsWithNull(int ProductTypeId)
        {
            return await _dappercontext.QueryAsync<Products>("select * from Products as p WHERE p.ProductTypeId = @ProductTypeId", new { ProductTypeId });
          //  return await _context.Products.Where(x => x.ProductTypeId == ProductTypeId).AsNoTracking().ToListAsync();
        }

        [HttpGet("GetPoducts")]
        public async Task<ActionResult<IEnumerable<Products>>> GetPoducts()
        {
            return await _dappercontext.QueryAsync<Products>("select Id, Name from Products");
            //return await _context.Products.Select(x => new Products { Id = x.Id, Name = x.Name }).AsNoTracking().ToListAsync();
        }
    }
}
