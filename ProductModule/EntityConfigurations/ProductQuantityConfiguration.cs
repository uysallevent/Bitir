using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductModule.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductModule.EntityConfigurations
{
    public class ProductQuantityConfiguration : IEntityTypeConfiguration<ProductQuantity>
    {
        private const string tableName = "ProductQuantity";
        private const string schemaName = "product";
        public void Configure(EntityTypeBuilder<ProductQuantity> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.UnitId).IsRequired();
            builder.HasData(new ProductQuantity[] {
                new ProductQuantity
                    {
                        Id=1,
                        Quantity=1,
                        UnitId=1,
                        Status=Core.Enums.Status.Active,
                        InsertDate=DateTime.Now,
                        UpdateDate=DateTime.Now
                    }
            });

        }
    }
}
