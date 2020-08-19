using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Blazored.LocalStorage;
using CentreAppBlazor.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Globalization;
using Radzen;

namespace CentreAppBlazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mjc5NzQzQDMxMzgyZTMxMmUzMG11bXVjN0xyYmprdUxKOCsvUjJvVCtxSXJ4MXNyb014SlBjRURnbVpMTGc9");
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
           
            builder.Services.AddTransient(sp => new HttpClient 
            { 
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress),
            });
            LibServices(builder);
            SetServices(builder);

                var culture = new CultureInfo("ru");
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            
            await builder.Build().RunAsync();
        }
        private static void LibServices(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
        }
        private static void SetServices(WebAssemblyHostBuilder builder)
        {
            builder.Services.AddTransient<TokenService>();
            builder.Services.AddTransient<AuthService>();
            builder.Services.AddScoped<IAppService, AppService>();

            builder.Services.AddScoped<AuthenticationStateProvider, LocalStorageAuthProvider>();

            builder.Services.AddSingleton<LoadingService>();
        }
    }
}
