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
            new ProvinceConfiguration().Configure(modelBuilder.Entity<Province>());
            new DistrictConfiguration().Configure(modelBuilder.Entity<District>());
            new NeighbourhoodConfiguration().Configure(modelBuilder.Entity<Neighbourhood>());
        }
    }
}
