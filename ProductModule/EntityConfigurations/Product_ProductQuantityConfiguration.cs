using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductModule.Entities;
using System;

namespace ProductModule.EntityConfigurations
{
    public class Product_ProductQuantityConfiguration : IEntityTypeConfiguration<Product_ProductQuantity>
    {
        private const string tableName = "Product_ProductQuantity";
        private const string schemaName = "product";
        public void Configure(EntityTypeBuilder<Product_ProductQuantity> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.ProductQuantityId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.InsertDate).IsRequired();
            builder.Property(x => x.UpdateDate).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.HasData(new Product_ProductQuantity[] {
                new Product_ProductQuantity
                    {
                        Id=1,
                        ProductId=1,
                        ProductQuantityId=1,
                        Status=Core.Enums.Status.Active,
                        InsertDate=DateTime.Now,
                        UpdateDate=DateTime.Now
                    }
            });

        }
    }
}
