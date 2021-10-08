using AuthModule.Business;
using AuthModule.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Shared;
using Module.Shared.Entities.SalesModuleEntities;

namespace SalesModule
{
    public class Startup : IStartup
    {
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddScoped<ICarrierBusinessBase<Carrier>, CarrierBusinessBase>();
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {

        }
    }
}