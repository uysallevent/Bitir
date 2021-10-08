using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Module.Shared.Entities.ProductModuleEntities.Configuration
{
    public class ProductStockConfiguration : IEntityTypeConfiguration<ProductStock>
    {
        private const string tableName = "ProductStock";
        private const string schemaName = "product";
        public void Configure(EntityTypeBuilder<ProductStock> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.ProductStoreId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
        }
    }
}
