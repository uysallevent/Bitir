using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Module.Shared.Entities.ProductModuleEntities.Configuration
{
    public class ProductPriceConfiguration : IEntityTypeConfiguration<ProductStorePrice>
    {
        private const string tableName = "ProductPrice";
        private const string schemaName = "product";
        public void Configure(EntityTypeBuilder<ProductStorePrice> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.ProductStoreId).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.HasOne(x => x.ProductStore).WithMany(x => x.ProductStorePrices).HasForeignKey(x => x.ProductStoreId);
        }
    }
}
