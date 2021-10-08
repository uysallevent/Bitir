using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Module.Shared.Entities.ProductModuleEntities.Configuration
{
    public class ProductQuantityConfiguration : IEntityTypeConfiguration<ProductQuantity>
    {
        private const string tableName = "Category";
        private const string schemaName = "product";
        public void Configure(EntityTypeBuilder<ProductQuantity> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.UnitId).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
        }
    }
}
