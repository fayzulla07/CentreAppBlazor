using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Server.ReportToExcel;
using CentreAppBlazor.Shared.Domain;
using CentreAppBlazor.Shared.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace CentreAppBlazor.Server.Controllers
{
    [Route("api/[controller]")]
   
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly DapperContext _dapperContext;
        private IReportToFile reportToFile;

        public ReportController(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        [Authorize]
        [HttpGet(@"GetRemains")]
        public async Task<ActionResult<IEnumerable<RemainReportDto>>> GetRemains()
        {
            return await _dapperContext.QueryAsync<RemainReportDto>("select p.ID as ProductId, p.Name,p.Code,v.IncomeCost,v.SaleCost,v.OptCost,p.RemainCount, p.Limit from Products as p left join AvCurrentCosts v on p.Id = v.ProductId where p.Id in (select productId from ProductIncoms) order by p.RemainCount desc; ");
        }

        [Authorize]
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

        [Authorize]
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

            saleret.sale = await _dapperContext.QueryAsync<SaleReportDto>("SELECT [Id],[Name],[Amount],[IncomeCost],[SaleCost], ([Amount] *[SaleCost]) as TotalSaleCost,[Client],[OptCost],[RegDt],[OrderNumber],[Comments],[IsBank] FROM[HistorySaleView] where RegDt between @da1 and @da2", new { da1, da2 });
            saleret.returns = await _dapperContext.QueryAsync<ReturnReportDto>("SELECT [Id],[Name],[ReturnCost],[Amount],[RegDt],[CustomerName] FROM[ReturnView] where RegDt between @da1 and @da2", new { da1, da2 });

            return saleret;
        }

        // -------------------------------------------------------------------------------------------------- //
        // Остаток 
        [Authorize]
        [HttpGet(@"GetRemainsExcel")]
        public async Task<FileStreamResult> GetRemainsExcel()
        {
            var remainReport = await _dapperContext.QueryAsync<RemainReportDto>("select p.ID as ProductId, p.Name,p.Code," +
                "v.IncomeCost,v.SaleCost,v.OptCost,p.RemainCount, p.Limit from Products as p left join AvCurrentCosts v " +
                "on p.Id = v.ProductId where p.Id in (select productId from ProductIncoms) order by p.RemainCount desc; ");

            reportToFile = new RemainReport();
            var excel = reportToFile.GetFile($"Остаток по дате: {TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("Central Asia Standard Time")).ToString("dd-MM-yyyy")}", remainReport);

            return new FileStreamResult(excel, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "Remains.xlsx"
            };
        }
         // Продажа
        [Authorize]
        [HttpGet(@"GetSaleExcel")]
        public async Task<FileStreamResult> GetSaleExcel([FromQuery] string das1, string das2)
        {
            DateTime da1;
            DateTime da2;

            if (!DateTime.TryParse(das1, out da1))
                  return null;
            if (!DateTime.TryParse(das2, out da2))
                  return null;

            SaleReturnReportDto saleret = new SaleReturnReportDto();

            saleret.sale = await _dapperContext.QueryAsync<SaleReportDto>("SELECT [Id],[Name],[Amount],[IncomeCost], [SaleCost], ([Amount] *[SaleCost]) as TotalSaleCost,[Client],[OptCost],[RegDt],[OrderNumber],[Comments],[IsBank] FROM [HistorySaleView] where RegDt between @da1 and @da2", new { da1, da2 });
            saleret.returns = await _dapperContext.QueryAsync<ReturnReportDto>("SELECT [Id],[Name],[ReturnCost],[Amount],[RegDt],[CustomerName] FROM[ReturnView] where RegDt between @da1 and @da2", new { da1, da2 });

            reportToFile = new SalesReport();
            var excel = reportToFile.GetFile($"Продажа по дате: {da1.ToShortDateString()} -  {da2.ToShortDateString()}", saleret);

            return new FileStreamResult(excel, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "Sale.xlsx"
            };
        }

        // Приход
        [Authorize]
        [HttpGet(@"GetIncomeExcel")]
        public async Task<FileStreamResult> GetIncomeExcel([FromQuery] string das1, string das2)
        {
            DateTime da1;
            DateTime da2;

            if (!DateTime.TryParse(das1, out da1))
                return null;
            if (!DateTime.TryParse(das2, out da2))
                return null;

            IEnumerable<IncomeReportDto> data = new List<IncomeReportDto>();
            data = await _dapperContext.QueryAsync<IncomeReportDto>("select pin.Id, p.ID as ProductId, p.Name,p.Code,s.Name as SupplierName, pin.IncomeCost, " +
                 "pin.SaleCost, pin.OptCost, pin.RegDt, u.Name as UserName, pin.Amount from ProductIncoms pin inner join Products p on p.Id = pin.ProductId left " +
                 "join Users u on pin.UserId = u.Id left join Suppliers s on pin.SupplierId = s.Id where RegDt between @da1 and @da2", new { da1, da2 });
            reportToFile = new IncomeReport();
            var excel = reportToFile.GetFile($"Приход по дате: {da1.ToShortDateString()} -  {da2.ToShortDateString()}", data);

            return new FileStreamResult(excel, new MediaTypeHeaderValue("text/plain"))
            {
                FileDownloadName = "Income.xlsx"
            };
        }
    }
}
