using AutoMapper;
using CentreAppBlazor.Server.Data;
using CentreAppBlazor.Shared;
using CentreAppBlazor.Shared.Auth;
using CentreAppBlazor.Shared.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CentreAppBlazor.Server.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly TechContext _context;
        private readonly IMapper _mapper;
        public AuthService(IConfiguration configuration, TechContext context, IMapper mapper)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }
        private UserResponse CreateToken(Users user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AuthSettings:Key").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(Convert.ToInt32(_configuration.GetSection("AuthSettings:ExpiresMin").Value)),
                SigningCredentials = creds,
                Audience = _configuration.GetSection("AuthSettings:Audience").Value,
                Issuer = _configuration.GetSection("AuthSettings:Issuer").Value
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return new UserResponse { Token = tokenHandler.WriteToken(token), ExpiredDate = tokenDescriptor.Expires, Name = user.Name, UserName = user.LoginName, Role = user.Role.Name } ;
        }
        
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Name.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        public async Task<int> Register(Users user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<UserResponse> Login(LoginViewModel model)
        {
            UserResponse response = null;
            Users user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.LoginName.ToLower().Equals(model.UserName.ToLower()) && x.Password.Equals(model.Password));
            if (user != null)
            {
                response = CreateToken(user);
                // response.Message = "Имя или пароль не верны. Попробуйте еще раз!";

            }
            else
                response = new UserResponse();
            return response;
        }
    }
}
