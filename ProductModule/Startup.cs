using Bitir.Data.Contexts;
using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.DataAccess.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Shared;
using Module.Shared.Entities.ProductModuleEntities;
using Module.Shared.Entities.SalesModuleEntities;
using ProductModule.Business;
using ProductModule.Interfaces;

namespace ProductModule
{
    public class Startup : IStartup
    {
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddScoped<IProductService<Product>, ProductBusinessBase>();
            services.AddScoped<IRepository<Category>, EFRepository<Category, BitirMainContext>>();
            services.AddScoped<IRepository<Product>, EFRepository<Product, BitirMainContext>>();
            services.AddScoped<IRepository<ProductStorePrice>, EFRepository<ProductStorePrice, BitirMainContext>>();
            services.AddScoped<IRepository<ProductStock>, EFRepository<ProductStock, BitirMainContext>>();
            services.AddScoped<IRepository<Product_Store>, EFRepository<Product_Store, BitirMainContext>>();
            services.AddScoped<IRepository<ProductQuantity>, EFRepository<ProductQuantity, BitirMainContext>>();
            services.AddScoped<IRepository<Unit>, EFRepository<Unit, BitirMainContext>>();
            services.AddScoped<IRepository<StoreOrdersView>, EFRepository<StoreOrdersView, BitirMainContext>>();
            services.AddScoped<IProcedureExecuter<StoreProductViewModel>, ProcedureExecuter<StoreProductViewModel, BitirMainContext>>();
            services.AddScoped<IProcedureExecuter<StoreProductByCarrierViewModel>, ProcedureExecuter<StoreProductByCarrierViewModel, BitirMainContext>>();
            services.AddScoped<IProcedureExecuter<StoreProductByStoreViewModel>, ProcedureExecuter<StoreProductByStoreViewModel, BitirMainContext>>();

        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
        {

        }
    }
}