using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Module.Shared.Entities.SalesModuleEntities.Configuration
{
    public class StoreOrdersViewConfiguration : IEntityTypeConfiguration<StoreOrdersView>
    {
        private const string tableName = "StoreOrdersView";
        private const string schemaName = "sales";
        public virtual void Configure(EntityTypeBuilder<StoreOrdersView> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.OrderId);
        }
    }
}
