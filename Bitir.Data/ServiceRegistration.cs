using Bitir.Data.Contexts;
using Bitir.Data.EntityConfigurations;
using Bitir.Data.Interfaces;
using Core.DataAccess.EntityFramework;
using Core.DataAccess.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.Shared.Entities.ProductModuleEntities;

namespace Bitir.Data
{
    public static class ServiceRegistration
    {
        public static void AddDataServices(this IServiceCollection serviceCollection, IConfiguration configuration = null)
        {
            var connectionString = configuration.GetSection("ConnectionString:BitirMainContext").Value;
            serviceCollection.AddDbContext<BitirMainContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork<BitirMainContext>>();
            serviceCollection.AddScoped<IExecuteProcedure<StoreProductViewModel>, ExecuteProcedure<StoreProductViewModel>>();
        }
    }
}
