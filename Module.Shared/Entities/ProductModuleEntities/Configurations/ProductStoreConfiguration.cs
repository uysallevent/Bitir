using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Module.Shared.Entities.ProductModuleEntities.Configuration
{
    public class ProductStoreConfiguration : IEntityTypeConfiguration<Product_Store>
    {
        private const string tableName = "ProductStore";
        private const string schemaName = "product";
        public void Configure(EntityTypeBuilder<Product_Store> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.StoreId).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.HasOne(x => x.Product).WithMany(x => x.ProductStores).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Store).WithMany(x => x.ProductStores).HasForeignKey(x => x.StoreId);
            builder.HasMany(x => x.ProductStorePrices).WithOne(x => x.ProductStore).HasForeignKey(x => x.ProductStoreId);
        }
    }
}
