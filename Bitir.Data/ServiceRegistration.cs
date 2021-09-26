using AuthModule.Entities;
using Bitir.Data.Contexts;
using Core.DataAccess;
using Core.DataAccess.EntityFramework;
using Core.DataAccess.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductModule.Entities;
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
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork<BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<UserAccount>, EFRepository<UserAccount, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<UserToken>, EFRepository<UserToken, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<Product>, EFRepository<Product, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<Category>, EFRepository<Category, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<Unit>, EFRepository<Unit, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<ProductPrice>, EFRepository<ProductPrice, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<ProductStock>, EFRepository<ProductStock, BitirMainContext>>();
            serviceCollection.AddScoped<IRepository<AccountType>, EFRepository<AccountType, BitirMainContext>>();


        }
    }
}
