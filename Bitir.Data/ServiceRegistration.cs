using AuthModule.Entities;
using Bitir.Data.Contexts;
using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.DataAccess.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitir.Data
{
    public static class ServiceRegistration
    {
        public static void AddDataServices(this IServiceCollection serviceCollection, IConfiguration configuration = null)
        {
            var connectionString = configuration.GetSection("ConnectionString:BitirMainContext").Value;
            serviceCollection.AddDbContext<BitirMainContext>(options =>options.UseSqlServer(connectionString));
            serviceCollection.AddScoped<IRepository<UserAccount>, EFRepository<UserAccount, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<UserToken>, EFRepository<UserToken, BitirMainContext>>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork<BitirMainContext>>();


        }
    }
}
