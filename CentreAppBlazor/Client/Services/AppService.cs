using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Text.Json;
namespace CentreAppBlazor.Client.Services
{
    public class AppService: IAppService
    {
        private readonly HttpClient _client;
        private readonly TokenService _token;

        public AppService(HttpClient client, TokenService token)
        {
            _client = client;
            _token = token;
        }
        //---  Get generic ---
        async Task<string> SetToken()
        {
            string TokenStr = await _token.GetToken();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenStr);
            return TokenStr;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            if (string.IsNullOrEmpty(await SetToken())) return default(T);
            return await _client.GetFromJsonAsync<T>($"/{uri}");
        }
        public async Task UpdateAsync<T>(T obj, int id, string uri)
        {
            if (string.IsNullOrEmpty(await SetToken())) return;
            await _client.PutAsJsonAsync<T>($"/{uri}/{id}", obj); 
        }
        public async Task<T> InsertAsync<T>(T obj, string url)
        {
            if (string.IsNullOrEmpty(await SetToken())) return default(T);
            var responce = await _client.PostAsJsonAsync<T>($"/{url}", obj);
            return await responce.Content.ReadFromJsonAsync<T>();
        }
        public async Task DeleteAsync(int Id, string url)
        {
            if (string.IsNullOrEmpty(await SetToken())) return;
          var response = await _client.DeleteAsync($"/{url}/{Id}");
            response.EnsureSuccessStatusCode();
        }
        public async Task<T> GetByIdAsync<T>(int id, string uri)
        {
            if (string.IsNullOrEmpty(await SetToken())) return default(T);

            return await _client.GetFromJsonAsync<T>($"/{uri}/{id}");
        }


        public async Task<T> PostAsync<T>(object obj, string url)
        {
            if (string.IsNullOrEmpty(await SetToken())) return default(T);
            var responce = await _client.PostAsJsonAsync($"/{url}", obj);
            return await responce.Content.ReadFromJsonAsync<T>();
        }
        public async Task<string> PostGetStringAsync<T>(object obj, string url)
        {
            if (string.IsNullOrEmpty(await SetToken())) return "";
            var responce = await _client.PostAsJsonAsync($"/{url}", obj);
            return await responce.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> CallAsync(string uri)
        {
            await SetToken();
           return await _client.GetAsync($"/{uri}");
        }
        public async Task<T> GetJustAsync<T>(string uri)
        {
            if (string.IsNullOrEmpty(await SetToken())) return default(T);
            return await _client.GetFromJsonAsync<T>($"/{uri}");
        }
    }
}
