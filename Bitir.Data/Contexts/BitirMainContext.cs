using AuthModule.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bitir.Data.Contexts
{
    public class BitirMainContext:DbContext
    {
        public BitirMainContext(DbContextOptions<BitirMainContext> dbContextOptions):base(dbContextOptions)
        {

        }

        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<UserToken> UserToken { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().ToTable("UserAccount","auth").HasKey(x => x.Id);
            modelBuilder.Entity<UserToken>().ToTable("UserToken", "auth").HasKey(x => x.Id);
        }
    }
}
