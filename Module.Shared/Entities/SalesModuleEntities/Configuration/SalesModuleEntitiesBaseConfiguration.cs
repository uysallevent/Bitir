using Microsoft.EntityFrameworkCore;


namespace Module.Shared.Entities.SalesModuleEntities.Configuration
{
    public class SalesModuleEntitiesBaseConfiguration
    {
        public virtual void Configure(ModelBuilder modelBuilder)
        {
            new Carrier_StoreConfiguration().Configure(modelBuilder.Entity<Carrier_Store>());
            new CarrierConfiguration().Configure(modelBuilder.Entity<Carrier>());
            new Store_UserAccountConfiguration().Configure(modelBuilder.Entity<Store_UserAccount>());
            new StoreConfiguration().Configure(modelBuilder.Entity<Store>());
            new OrderConfiguration().Configure(modelBuilder.Entity<Order>());
            new StoreOrdersViewConfiguration().Configure(modelBuilder.Entity<StoreOrdersView>());
        }
    }
}
