using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Shared.Domain;
using CentreAppBlazor.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CentreAppBlazor.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GetTotalsController : ControllerBase
    {
        private readonly TechContext _context;
        private readonly IMapper _mapper;

        public GetTotalsController(TechContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // GET: api/<GetTotalsController>
        [HttpGet]
        public async Task<SalesReturnsbyDayDto> Get()
        {
            SalesReturnsbyDayDto result = new SalesReturnsbyDayDto();

            var salestotal = await _context.SalesTotalByDayView.Where(x => x.RegDt.Value.Date == DateTime.Now.Date).ToListAsync();
            var returnstotal = await _context.ReturnTotalByDayView.Where(x => x.RegDt.Value.Date == DateTime.Now.Date).ToListAsync();

            if(salestotal.FirstOrDefault() != null)
             result = _mapper.Map<SalesReturnsbyDayDto>(salestotal.FirstOrDefault());


            if(returnstotal.Count > 0)
            result.ReturnCost = returnstotal.FirstOrDefault().ReturnCost;


            if (User.IsInRole("admin"))
            {
                var profit = await _context.AvProfit.AsNoTracking().FirstOrDefaultAsync();
                if (profit != null)
                    result.AvProfit = profit;
            }
            else
            {
                result.IncomeCost = 0;
            }
            return result;
        }

        [HttpPost("GetSalesbyDay")]
        public async Task<ActionResult<SalesReturnsbyDayDto>> GetSalesbyDay([FromBody] DateTime time)
        {
            if (time == null) return BadRequest("the value has been null");
            var salestotal = await _context.SalesTotalByDayView.Where(x => x.RegDt.Value.Date == time.Date).ToListAsync();
            var returnstotal = await _context.ReturnTotalByDayView.Where(x => x.RegDt.Value.Date == time.Date).ToListAsync();

            SalesReturnsbyDayDto result = _mapper.Map<SalesReturnsbyDayDto>(salestotal.FirstOrDefault());

            if (result == null)
               return new SalesReturnsbyDayDto() {  RegDt = time };

            if (returnstotal.Count > 0)
                result.ReturnCost = returnstotal.FirstOrDefault().ReturnCost;

            return result;
        }

    }
}
