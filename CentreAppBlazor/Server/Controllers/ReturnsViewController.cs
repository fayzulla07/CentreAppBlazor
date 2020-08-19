using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Shared.Domain;
using Microsoft.AspNetCore.Authorization;
using CentreAppBlazor.Shared.Dto;
using System.Data.SqlClient;
using System;

namespace CentreAppBlazor.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnsViewController : ControllerBase
    {
        private readonly DapperContext _dappercontext;
        private readonly TechContext _context;

        public ReturnsViewController(DapperContext dappercontext, TechContext context)
        {
            _dappercontext = dappercontext;
            _context = context;
        }

        // GET: api/ReturnsView
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReturnView>>> GetReturnsView()
        {
            return await _context.ReturnView.ToListAsync();
        }

        // DELETE: api/ReturnsView/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReturnView>> DeleteUnits(int id)
        {
            try
            {
                await _dappercontext.ExecuteAsync("SP_DeleteProductReturn", new { Id = id }, CommandType: System.Data.CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                return BadRequest();

            }
            return new ReturnView();
        }
    }
}
