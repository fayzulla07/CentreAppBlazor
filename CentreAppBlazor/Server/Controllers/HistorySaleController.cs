using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using CentreAppBlazor.Shared.Dto;
using System;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Claims;
using System.Linq.Dynamic.Core;
using Dapper;
using System.IO;
using Microsoft.Net.Http.Headers;

namespace CentreAppBlazor.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HistorySaleController : ControllerBase
    {
        private readonly TechContext _context;
        private readonly DapperContext _dappercontext;

        public HistorySaleController(TechContext context, DapperContext dappercontext)
        {
            _context = context;
            _dappercontext = dappercontext;
        }

        // GET: api/HistorySale
        [HttpGet]
        public async Task<ActionResult<ResponseMessage<IEnumerable<HistorySaleView>>>> GetHistorySale([FromQuery] ApiQuery query)
        {
            if (query == null)
                return BadRequest();
            var result = _context.HistorySaleView.OrderByDescending(x=>x.RegDt).AsQueryable();
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
            return new ResponseMessage<IEnumerable<HistorySaleView>>() { entity = await result.ToListAsync(), TCount = _context.HistorySaleView.Count() };
        }


        [HttpPost]
        public async Task<ActionResult<bool>> PostHistorySaleView(HistorySaleView entity)
        {
            try
            {
                if (!ModelState.IsValid) BadRequest();
                var p = new DynamicParameters();

                p.Add("@ProductSaleId", entity.Id);
                p.Add("@Amount", entity.Amount);
                p.Add("@RegDT", entity.RegDt);
                p.Add("@UserID", int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value));
                p.Add("@Comments", entity.Comments);
                int result = await _dappercontext.ExecuteAsync("SP_AddProductReturn", p, CommandType: System.Data.CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

            return true;
        }

        // DELETE: api/Units/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUnits(int id)
        {
            if(id < 0)
            {
                return BadRequest();
            }
              await _dappercontext.ExecuteAsync("SP_DeleteProductSale", new { Id = id }, CommandType: System.Data.CommandType.StoredProcedure);

            return Ok();
        }
        [AllowAnonymous]
        [HttpGet("GetInvoiceSale/{Id}")]
        public async Task<FileStreamResult> GetInvoice(int Id)
        {
            var productSales = await _context.ProductSales.Include(x => x.Product).ThenInclude(x => x.Unit).Where(p => p.OrderNumber == Id).ToListAsync();
            Services.Print print = new Services.Print();
            var excel = print.GetSaleFile(productSales);
            Stream stream = new MemoryStream(excel);
            return new FileStreamResult(stream, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "IncomeInvoice.xlsx"
            };
        }
    }
}
