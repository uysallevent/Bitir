using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Module.Shared.Entities.ProductModuleEntities.Configuration
{
    public class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
    {
        private const string tableName = "ProductPrice";
        private const string schemaName = "product";
        public void Configure(EntityTypeBuilder<ProductPrice> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.ProductStoreId).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
        }
    }
}
