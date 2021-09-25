using AuthModule.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;
using IStartUp = Bitir.Api.Module.Shared.IStartup;

namespace ModulerApi.AuthModule
{
    public class Startup : IStartUp
    {
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["TokenOptions:Issuer"],
                    ValidAudience = Configuration["TokenOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["TokenOptions:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero,
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            //var serviceProvider = services.BuildServiceProvider();
                            //var service = serviceProvider.GetService<AuthBusinessBase>();
                            //_ = service.RefreshTokenLogin(1, "zvyZ8g8oa4beUzeJFRhJXKasw4WgZzFENWSiL1XohyA=");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization();
            services.AddSingleton<ITokenHelper, JwtHelper>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEndpoints(endpoints =>
                endpoints.MapGet("/AuthModule",
                    async context =>
                    {
                        await context.Response.WriteAsync("Hello World from TestEndpoint in Module 2");
                    })
            );
        }
    }
}