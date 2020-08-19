using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Shared.Domain;
using Microsoft.AspNetCore.Authorization;

namespace CentreAppBlazor.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly TechContext _context;

        public RolesController(TechContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRoles(int id)
        {
            var roles = await _context.Roles.FindAsync(id);

            if (roles == null)
            {
                return NotFound();
            }

            return roles;
        }
    }
}
