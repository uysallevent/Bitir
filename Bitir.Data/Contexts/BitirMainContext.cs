using AuthModule.Entities;
using AuthModule.EntityConfigurations;
using Bitir.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using ProductModule.Entities;
using ProductModule.EntityConfigurations;

namespace Bitir.Data.Contexts
{
    public class BitirMainContext : DbContext
    {
        public BitirMainContext(DbContextOptions<BitirMainContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<UserToken> UserToken { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<ProductPrice> ProductPrice { get; set; }
        public DbSet<ProductStock> ProductStock { get; set; }
        public DbSet<AccountType> AccountType { get; set; }
        public DbSet<ProductQuantity> ProductQuantity { get; set; }
        public DbSet<Product_ProductQuantity> Product_ProductQuantityConfiguration { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
            new UnitConfiguration().Configure(modelBuilder.Entity<Unit>());
            new ProductQuantityConfiguration().Configure(modelBuilder.Entity<ProductQuantity>());   
            new ProductPriceConfiguration().Configure(modelBuilder.Entity<ProductPrice>());
            new ProductStockConfiguraiton().Configure(modelBuilder.Entity<ProductStock>());
            new UserAccountConfiguration().Configure(modelBuilder.Entity<UserAccount>());
            new UserTokenConfiguration().Configure(modelBuilder.Entity<UserToken>());
            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
            new AccountTypeConfiguration().Configure(modelBuilder.Entity<AccountType>());
            new Product_ProductQuantityConfiguration().Configure(modelBuilder.Entity<Product_ProductQuantity>());
        }
    }
}
