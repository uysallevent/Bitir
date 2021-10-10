using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace Module.Shared.Entities.ProductModuleEntities.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        private const string tableName = "Product";
        private const string schemaName = "product";
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
            builder.HasData(new Product[]
            {
                new Product
                {
                    Id=1,
                    Name="Süt",
                    CategoryId=1,
                    Description="Günlük İnek Sütü",
                    Status=Core.Enums.Status.Active,
                    InsertDate=DateTime.Now,
                    UpdateDate=DateTime.Now
                }
            });
        }
    }
}
