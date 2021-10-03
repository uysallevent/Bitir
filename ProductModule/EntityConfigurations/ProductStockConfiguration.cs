using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductModule.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductModule.EntityConfigurations
{
    public class ProductStockConfiguraiton : IEntityTypeConfiguration<ProductStock>
    {
        private const string tableName = "ProductStock";
        private const string schemaName = "product";
        public void Configure(EntityTypeBuilder<ProductStock> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.MerchantId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
        }
    }
}
