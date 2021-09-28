using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Shared;
using ProductModule.Business;
using ProductModule.Entities;
using ProductModule.Interfaces;

namespace ProductModule
{
    public class Startup : IStartup
    {
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddScoped<IProductBusinessBase<Product>, ProductBusinessBase>();
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
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