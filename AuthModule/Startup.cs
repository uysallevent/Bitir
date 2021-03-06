using AuthModule.Business;
using AuthModule.Interfaces;
using AuthModule.Security.JWT;
using BaseModule.Business;
using BaseModule.Interfaces;
using Bitir.Data.Contexts;
using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Module.Shared.Entities.AuthModuleEntities;
using System;
using System.Text;
using System.Threading.Tasks;


namespace AuthModule
{
    public class Startup : Module.Shared.IStartup
    {

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration = null)
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
                    ValidIssuer = configuration["TokenOptions:Issuer"],
                    ValidAudience = configuration["TokenOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["TokenOptions:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero,
                };
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            var refreshToken = context.Request.Headers["RefreshToken"];
                            var serviceProvider = services.BuildServiceProvider();
                            var service = serviceProvider.GetService<IAuthBusinessBase<UserAccount>>();
                            _ = service.RefreshTokenLogin(refreshToken[0]);
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddHttpContextAccessor();
            services.AddScoped<IRepository<UserAccount>, EFRepository<UserAccount, BitirMainContext>>();
            services.AddScoped<IRepository<UserAddress>, EFRepository<UserAddress, BitirMainContext>>();
            services.AddScoped<IRepository<UserToken>, EFRepository<UserToken, BitirMainContext>>();
            services.AddScoped<IRepository<Province>, EFRepository<Province, BitirMainContext>>();
            services.AddScoped<IRepository<District>, EFRepository<District, BitirMainContext>>();
            services.AddScoped<IRepository<Neighbourhood>, EFRepository<Neighbourhood, BitirMainContext>>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddScoped<IBusinessBase<Province>, BusinessBase<Province>>();
            services.AddScoped<IBusinessBase<District>, BusinessBase<District>>();
            services.AddScoped<IBusinessBase<Neighbourhood>, BusinessBase<Neighbourhood>>();
            services.AddScoped<IAuthBusinessBase<UserAccount>, AuthBusinessBase>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}