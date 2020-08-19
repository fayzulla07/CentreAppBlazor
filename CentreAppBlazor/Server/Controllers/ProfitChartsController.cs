using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Shared.Charts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentreAppBlazor.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProfitChartsController : ControllerBase
    {
        private readonly DapperContext _dappercontext;

        public ProfitChartsController(DapperContext dappercontext)
        {
            _dappercontext = dappercontext;
        }
        [HttpGet]
        public async Task<IEnumerable<ProfitDto>> GetProfit()
        {
          return  await _dappercontext.QueryAsync<ProfitDto>("select SUM(SaleCost - IncomeCost) as Revenue, CONCAT(MONTH(RegDt),'-', YEAR(RegDt)) as RegDt from ProductSales GROUP BY MONTH(RegDt), YEAR(RegDt)");
        }
    }
}
