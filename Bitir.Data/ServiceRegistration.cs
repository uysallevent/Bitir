using Bitir.Data.Contexts;
using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.DataAccess.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Shared.Entities.AuthModuleEntities;
using Module.Shared.Entities.ProductModuleEntities;
using Module.Shared.Entities.SalesModuleEntities;

namespace Bitir.Data
{
    public static class ServiceRegistration
    {
        public static void AddDataServices(this IServiceCollection serviceCollection, IConfiguration configuration = null)
        {
            var connectionString = configuration.GetSection("ConnectionString:BitirMainContext").Value;
            serviceCollection.AddDbContext<BitirMainContext>(options =>options.UseSqlServer(connectionString));
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork<BitirMainContext>>();
            #region AuthModule
            serviceCollection.AddScoped<IRepository<UserAccount>, EFRepository<UserAccount, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<UserToken>, EFRepository<UserToken, BitirMainContext>>();
            #endregion

            #region ProductModule
            serviceCollection.AddScoped<IRepository<Category>, EFRepository<Category, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<Product>, EFRepository<Product, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<ProductPrice>, EFRepository<ProductPrice, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<ProductStock>, EFRepository<ProductStock, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<ProductStore>, EFRepository<ProductStore, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<ProductQuantity>, EFRepository<ProductQuantity, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<Unit>, EFRepository<Unit, BitirMainContext>>();
            #endregion

            #region SalesModule
            serviceCollection.AddScoped<IRepository<Carrier>, EFRepository<Carrier, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<Carrier_Store>, EFRepository<Carrier_Store, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<Store>, EFRepository<Store, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<Store_UserAccount>, EFRepository<Store_UserAccount, BitirMainContext>>();
            #endregion


        }
    }
}
