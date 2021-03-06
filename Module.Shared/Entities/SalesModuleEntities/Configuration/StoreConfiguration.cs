using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Module.Shared.Entities.SalesModuleEntities.Configuration
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        private const string tableName = "Store";
        private const string schemaName = "sales";
        public virtual void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
            builder.HasMany(x => x.Store_UserAccounts).WithOne(x => x.Store).HasForeignKey(x => x.StoreId);
            builder.HasMany(x => x.Carrier_Stores).WithOne(x => x.Store).HasForeignKey(x => x.StoreId);
            builder.HasMany(x => x.ProductStores).WithOne(x => x.Store).HasForeignKey(x => x.StoreId);
        }
    }
}
