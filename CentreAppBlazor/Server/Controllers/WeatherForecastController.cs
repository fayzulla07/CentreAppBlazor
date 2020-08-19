using CentreAppBlazor.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using CentreAppBlazor.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;
using Dapper;


namespace CentreAppBlazor.Server.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       

        public WeatherForecastController()
        {
           
        }
       
      
    }
}
