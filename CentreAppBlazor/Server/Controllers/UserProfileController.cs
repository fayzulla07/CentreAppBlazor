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
using System.Security.Claims;

namespace CentreAppBlazor.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly TechContext _context;

        public UserProfileController(TechContext context)
        {
            _context = context;
        }

        // GET: api/UserProfile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            var ID = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _context.Users.Where(x=>x.Id == int.Parse(ID)).ToListAsync();
        }


        // PUT: api/UserProfile/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.Id && id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value) || !ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
