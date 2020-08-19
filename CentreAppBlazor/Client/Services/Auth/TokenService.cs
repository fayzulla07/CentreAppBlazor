using CentreAppBlazor.Client.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;

using System.Threading.Tasks;

namespace CentreAppBlazor.Client.Services
{
    public class TokenService
    {
        private readonly NavigationManager _manager;
        private readonly LocalStorageAuthProvider _provider;

        public TokenService(NavigationManager manager, AuthenticationStateProvider provider)
        {
            _manager = manager;
            _provider = provider as LocalStorageAuthProvider;
        }
        public async Task<string> GetToken()
        {
            if(_provider.CurrentUser == null)
                return null;
            var CurrentUser = _provider.CurrentUser;
            if (CurrentUser.User == null)
            {
                await _provider.LogoutAsync();
                _manager.NavigateTo("/system/login");
                return null;
            }
            if (DateTime.Parse(CurrentUser.User.FindFirst("Expired").Value) < DateTime.Now)
            {
                await _provider.LogoutAsync();
                _manager.NavigateTo("/system/login");
                return null;
            }
            else
            {
                return CurrentUser.User.FindFirst("AccessToken").Value;
            }
        }
    }
}
