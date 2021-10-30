using Bitir.Data.Contexts;
using Core.DataAccess.EntityFramework;
using Core.DataAccess.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bitir.Data
{
    public static class ServiceRegistration
    {
        public static void AddDataServices(this IServiceCollection serviceCollection, IConfiguration configuration = null)
        {
            var connectionString = configuration.GetSection("ConnectionString:BitirMainContext").Value;
            serviceCollection.AddDbContext<BitirMainContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Scoped);
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork<BitirMainContext>>();
        }
    }
}
