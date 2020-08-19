using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Shared.Domain;
using CentreAppBlazor.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentreAppBlazor.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerMapController : ControllerBase
    {
        private readonly DapperContext _dappercontext;

        public CustomerMapController(DapperContext dappercontext)
        {
            _dappercontext = dappercontext;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomers(int id, Geo geo)
        {
            await _dappercontext.QueryAsync<Customers>("UPDATE Customers SET GeoLatitude = @lat, GeoLongitude = @lon WHERE Id = @Id;", new{ Id = id, lat = geo.latitude, lon = geo.longitude  });

            return NoContent();
        }
    }
}
