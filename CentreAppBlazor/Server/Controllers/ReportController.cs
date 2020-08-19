using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Shared.Report;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentreAppBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly DapperContext _dapperContext;

        public ReportController(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        [HttpGet(@"GetRemains")]
        public async Task<ActionResult<IEnumerable<RemainReportDto>>> GetRemains()
        {
            return await _dapperContext.QueryAsync<RemainReportDto>("select p.ID as ProductId, p.Name,p.Code,v.IncomeCost,v.SaleCost,v.OptCost,p.RemainCount, p.Limit from Products as p left join AvCurrentCosts v on p.Id = v.ProductId where p.Id in (select productId from ProductIncoms) order by p.RemainCount desc; ");
        }

        [HttpGet(@"GetIncome")]
        public async Task<ActionResult<IEnumerable<IncomeReportDto>>> GetIncome([FromQuery]string das1, string das2)
        {
            DateTime da1;
            DateTime da2;
            if(!DateTime.TryParse(das1, out da1))
                return BadRequest();
            if (!DateTime.TryParse(das2, out da2))
                return BadRequest();

            return await _dapperContext.QueryAsync<IncomeReportDto>("select pin.Id, p.ID as ProductId, p.Name,p.Code,s.Name as SupplierName, pin.IncomeCost, " +
                "pin.SaleCost, pin.OptCost, pin.RegDt, u.Name as UserName, pin.Amount from ProductIncoms pin inner join Products p on p.Id = pin.ProductId left " +
                "join Users u on pin.UserId = u.Id left join Suppliers s on pin.SupplierId = s.Id where RegDt between @da1 and @da2", new { da1, da2 });
        }

        [HttpGet(@"GetSale")]
        public async Task<ActionResult<SaleReturnReportDto>> GetSale([FromQuery] string das1, string das2)
        {
            DateTime da1;
            DateTime da2;

            if (!DateTime.TryParse(das1, out da1))
                return BadRequest();
            if (!DateTime.TryParse(das2, out da2))
                return BadRequest();

            SaleReturnReportDto saleret = new SaleReturnReportDto();

            saleret.sale = await _dapperContext.QueryAsync<SaleReportDto>("SELECT [Id],[Name],[Amount],[SaleCost], ([Amount] *[SaleCost]) as TotalSaleCost,[Client],[OptCost],[RegDt],[OrderNumber],[Comments],[IsBank] FROM[HistorySaleView] where RegDt between @da1 and @da2", new { da1, da2 });
            saleret.returns = await _dapperContext.QueryAsync<ReturnReportDto>("SELECT [Id],[Name],[ReturnCost],[Amount],[RegDt],[CustomerName] FROM[ReturnView] where RegDt between @da1 and @da2", new { da1, da2 });

            return saleret;
        }
    }
}
