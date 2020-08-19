using CentreAppBlazor.Shared;
using CentreAppBlazor.Shared.Auth;
using CentreAppBlazor.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentreAppBlazor.Server.Services.Auth
{
   public interface IAuthService
    {
        Task<int> Register(Users user);
        Task<UserResponse> Login(LoginViewModel model);
        Task<bool> UserExists(string username);
    }
}
