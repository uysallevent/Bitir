using AuthModule.Models;
using Bitir.Entity.TestDb.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Bitir.Dal.TestDal.Context
{
    public class TestContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.{envName}.json", true, true);
            var connectionString = builder.Build().GetSection("ConnectionStrings").GetSection("TestDbConnectionString").Value;
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>(entity =>
            {
                entity.ToTable("UserAccount","Auth");
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Email);
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.ToTable("UserToken", "Auth");
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "Product");
                entity.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Category", "Product");
                entity.HasKey(x => x.Id);
            });
        }

        public virtual DbSet<UserAccount> UserAccount { get; set; }
        public virtual DbSet<UserToken> UserToken { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Category> Category { get; set; }
    }
}
