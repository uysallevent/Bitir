using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Shared;
using Module.Shared.Entities.ProductModuleEntities;
using ProductModule.Business;
using ProductModule.Interfaces;

namespace ProductModule
{
    public class Startup : IStartup
    {
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services,IConfiguration configuration = null)
        {
            services.AddScoped<IProductService<Product>, ProductBusinessBase>();
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {

        }
    }
}