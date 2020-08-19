using CentreAppBlazor.Shared.Auth;
using System.Net.Http;
using System.Threading.Tasks;
using CentreAppBlazor.Client.Extensions;
using System.Net.Http.Json;

namespace CentreAppBlazor.Client.Services
{
    public class AuthService
    {
        private readonly HttpClient _client;


        public AuthService(HttpClient client)
        {
            _client = client;
        }

        public async Task<UserResponse> RegisterUserAsync(LoginViewModel request)
        {
            var response = await _client.PostAsJsonAsync<LoginViewModel>($"/api/auth/register", request);
            return await response.Content.ReadFromJsonAsync<UserResponse>();
        }


        public async Task<UserResponse> LoginUserAsync(LoginViewModel user)
        {
            UserResponse result = null;
                var responce = await _client.PostAsJsonAsync<LoginViewModel>("/api/auth/login", user);
            await responce.MyEnsureSuccessStatusCode();
            result = await responce.Content.ReadFromJsonAsync<UserResponse>();
            result.RememberMe = user.RememberMe;
            return result;
        }

        
    }
}
