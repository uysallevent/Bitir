using AuthModule.Entities;
using AuthModule.EntityConfigurations;
using Bitir.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using ProductModule.Entities;

namespace Bitir.Data.Contexts
{
    public class BitirMainContext : DbContext
    {
        public BitirMainContext(DbContextOptions<BitirMainContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<UserToken> UserToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new UserAccountConfiguration().Configure(modelBuilder.Entity<UserAccount>());
            new UserTokenConfiguration().Configure(modelBuilder.Entity<UserToken>());
            new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
            new ProductPriceConfiguration().Configure(modelBuilder.Entity<ProductPrice>());
            new ProductStockConfiguraiton().Configure(modelBuilder.Entity<ProductStock>());
            new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
            new UnitConfiguration().Configure(modelBuilder.Entity<Unit>());
            new AccountTypeConfiguration().Configure(modelBuilder.Entity<AccountType>());
        }
    }
}
