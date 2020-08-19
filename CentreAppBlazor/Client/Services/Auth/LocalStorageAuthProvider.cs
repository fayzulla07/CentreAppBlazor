using Blazored.LocalStorage;
using CentreAppBlazor.Shared.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CentreAppBlazor.Client.Services
{
    public class LocalStorageAuthProvider:AuthenticationStateProvider
    {
        private readonly ILocalStorageService _storageService;
        private readonly NavigationManager _manager;
        public bool FirstLogin { get; private set; }
        public AuthenticationState CurrentUser { get; private set; }

        public LocalStorageAuthProvider(ILocalStorageService storageService, NavigationManager manager)
        {
            _storageService = storageService;
            _manager = manager;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (await _storageService.ContainKeyAsync("User"))
            {
                var userInfo = await _storageService.GetItemAsync<UserResponse>("User");
                
                if (userInfo.ExpiredDate < DateTime.Now || (userInfo.RememberMe == false && !FirstLogin))
                {
                    await LogoutAsync();
                    return new AuthenticationState(new ClaimsPrincipal());
                }
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, userInfo.Name),
                    new Claim(ClaimTypes.NameIdentifier, userInfo.UserName),
                    new Claim("AccessToken", userInfo.Token),
                    new Claim("Expired", userInfo.ExpiredDate.ToString()),
                    new Claim("RememberMe", userInfo.RememberMe.ToString()),
                    new Claim(ClaimTypes.Role, userInfo.Role)
                };

                var identity = new ClaimsIdentity(claims, "Bearer");
                var user = new ClaimsPrincipal(identity);
                CurrentUser = new AuthenticationState(user);
                NotifyAuthenticationStateChanged(Task.FromResult(CurrentUser));
                return CurrentUser;
            }

            return new AuthenticationState(new ClaimsPrincipal());
        }

        public async Task LogoutAsync()
        {
            await _storageService.RemoveItemAsync("User");
            CurrentUser = null;
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
        }

        public async Task SetItemAsync(UserResponse userInfo)
        {
            FirstLogin = true;
            await _storageService.SetItemAsync("User", userInfo);
        }
    }
}
