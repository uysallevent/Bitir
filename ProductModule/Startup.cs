using BaseModule.Business;
using BaseModule.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductModule.Business;
using ProductModule.Entities;
using ProductModule.Interfaces;

namespace ProductModule
{
    public class Startup : Bitir.Api.Module.Shared.IStartup
    {
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductBusinessBase<Product>, ProductBusinessBase>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseEndpoints(endpoints =>
                 endpoints.MapGet("/Product",
                     async context =>
                     {
                         await context.Response.WriteAsync("Hello World from TestEndpoint in Module 2");
                     })
             );
        }
    }
}