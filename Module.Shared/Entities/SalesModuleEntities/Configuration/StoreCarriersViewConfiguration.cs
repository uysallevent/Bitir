using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Module.Shared.Entities.SalesModuleEntities.Configuration
{
    public class StoreCarriersViewConfiguration : IEntityTypeConfiguration<StoreCarriersView>
    {
        private const string tableName = "StoreCarriersView";
        private const string schemaName = "sales";
        public virtual void Configure(EntityTypeBuilder<StoreCarriersView> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.CarrierId);
        }
    }
}
