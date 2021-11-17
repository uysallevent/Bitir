using AuthModule.Business;
using AuthModule.Interfaces;
using BaseModule.Business;
using BaseModule.Interfaces;
using Bitir.Data.Contexts;
using Core.DataAccess;
using Core.DataAccess.EntityFramework;
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
            services.AddScoped<IOrderBusinessBase<Order>, OrderBusinessBase>();
            services.AddScoped<IRepository<Carrier>, EFRepository<Carrier, BitirMainContext>>();
            services.AddScoped<IRepository<CarrierDistributionZone>, EFRepository<CarrierDistributionZone, BitirMainContext>>();
            services.AddScoped<IRepository<Carrier_Store>, EFRepository<Carrier_Store, BitirMainContext>>();
            services.AddScoped<IRepository<Store>, EFRepository<Store, BitirMainContext>>();
            services.AddScoped<IRepository<Store_UserAccount>, EFRepository<Store_UserAccount, BitirMainContext>>();
            services.AddScoped<IRepository<Order>, EFRepository<Order, BitirMainContext>>();
            services.AddScoped<IRepository<OrderDetail>, EFRepository<OrderDetail, BitirMainContext>>();
            services.AddScoped<IRepository<StoreOrdersView>, EFRepository<StoreOrdersView, BitirMainContext>>();
            services.AddScoped<IRepository<StoreCarriersView>, EFRepository<StoreCarriersView, BitirMainContext>>();
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {

        }
    }
}