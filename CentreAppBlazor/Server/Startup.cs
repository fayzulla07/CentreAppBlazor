using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using CentreAppBlazor.Shared;

using Microsoft.EntityFrameworkCore;
using CentreAppBlazor.Server.Data;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using CentreAppBlazor.Server.Services.Auth;
using AutoMapper;
namespace CentreAppBlazor.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            DapperContext.ConStr = Configuration.GetConnectionString("DefaultConnection");
            RegisterCustomServices(services);
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            services.AddRazorPages();

            // добавляем контекст MobileContext в качестве сервиса в приложение
            services.AddDbContext<TechContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "CentreApp API", Version = "v1" });
                // Swagger 2.+ support
                var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

                x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                            Description = "JWT Authorization header using the Bearer scheme.",
                            Name = "Authorization",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.ApiKey,
                            Scheme = "Bearer"
                           
                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                     {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header

                        },
                            new List<string>()
                     }
                 });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                        .GetBytes(Configuration.GetSection("AuthSettings:Key").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                };
            });
        }

        private void RegisterCustomServices(IServiceCollection services)
        {
            // AutorizationService
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<DapperContext>();


        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
