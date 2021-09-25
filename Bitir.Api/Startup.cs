using AuthModule.Dal;
using AuthModule.Interfaces;
using Bitir.Business;
using Bitir.Business.Interfaces;
using Bitir.Dal.TestDal;
using Bitir.Dal.TestDal.Context;
using Bitir.Dal.TestDal.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Bitir.Api
{
    public class Startup : AuthModule.AuthModule
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            services.AddControllers();
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

            services.AddScoped<IUserAccountDal, UserAccountDal<TestContext>>();
            services.AddScoped<IUserTokenDal, UserTokenDal<TestContext>>();
            services.AddSingleton<ICategoryDal, CategoryDal>();
            services.AddScoped<ICategoryBusiness, CategoryBusiness>();
        }

        public override void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            base.Configure(app, env);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
