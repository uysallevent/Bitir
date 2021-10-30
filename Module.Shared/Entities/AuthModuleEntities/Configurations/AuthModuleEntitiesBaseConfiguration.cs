using Microsoft.EntityFrameworkCore;


namespace Module.Shared.Entities.AuthModuleEntities.Configuration
{
    public class AuthModuleEntitiesBaseConfiguration
    {
        public virtual void Configure(ModelBuilder modelBuilder)
        {
            new UserAccountConfiguration().Configure(modelBuilder.Entity<UserAccount>());
            new UserTokenConfiguration().Configure(modelBuilder.Entity<UserToken>());
            new UserAddressConfiguration().Configure(modelBuilder.Entity<UserAddress>());
        }
    }
}
