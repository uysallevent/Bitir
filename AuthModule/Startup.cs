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
                        //var serviceProvider = services.BuildServiceProvider();
                        //var service = serviceProvider.GetService<IClaimAccessor>();
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {

                            //_ = service.RefreshTokenLogin(1, "zvyZ8g8oa4beUzeJFRhJXKasw4WgZzFENWSiL1XohyA=");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            services.AddSingleton<ITokenHelper, JwtHelper>();
            services.AddScoped<IAuthBusinessBase<UserAccount>, AuthBusinessBase>();
            services.AddHttpContextAccessor();
            services.AddScoped<IRepository<UserAccount>, EFRepository<UserAccount, BitirMainContext>>();
            services.AddScoped<IRepository<UserAddress>, EFRepository<UserAddress, BitirMainContext>>();
            services.AddScoped<IRepository<UserToken>, EFRepository<UserToken, BitirMainContext>>();
            services.AddScoped<IRepository<Province>, EFRepository<Province, BitirMainContext>>();
            services.AddScoped<IRepository<District>, EFRepository<District, BitirMainContext>>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

        }
    }
}