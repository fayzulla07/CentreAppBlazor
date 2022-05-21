using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CentreAppBlazor.Client.Services
{
    public class SettingService
    {
        private readonly HttpClient http;
        public SettingService(HttpClient http)
        {
            this.http = http;
        }
/*        public async Task<Settings> GetTitleAsync()
        {
            return await http.GetFromJsonAsync<Settings>("Settings.json");
        }*/
    }
}
