using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentreAppBlazor.Server.Services.Auth;
using CentreAppBlazor.Shared;
using CentreAppBlazor.Shared.Auth;
using CentreAppBlazor.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CentreAppBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(Users request)
        {
            #warning Register action must be changed
            int response = await _service.Register(new Users { Name = request.Name, Password = request.Password });
            if (response == 0)
            {
                return BadRequest();
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserResponse response = await _service.Login(model);
                if (String.IsNullOrEmpty(response.Token))
                {
                    return StatusCode(401, new { errors = "Имя или пароль не верны. Попробуйте еще раз!" });
                }
                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}