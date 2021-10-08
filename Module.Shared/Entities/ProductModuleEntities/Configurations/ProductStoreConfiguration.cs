using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Module.Shared.Entities.ProductModuleEntities.Configuration
{
    public class ProductStoreConfiguration : IEntityTypeConfiguration<ProductStore>
    {
        private const string tableName = "ProductStore";
        private const string schemaName = "product";
        public void Configure(EntityTypeBuilder<ProductStore> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.StoreId).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
        }
    }
}
