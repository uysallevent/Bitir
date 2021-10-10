using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;

namespace Module.Shared.Entities.ProductModuleEntities.Configuration
{
    public class ProductQuantityConfiguration : IEntityTypeConfiguration<ProductQuantity>
    {
        private const string tableName = "ProductQuantity";
        private const string schemaName = "product";
        public void Configure(EntityTypeBuilder<ProductQuantity> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.UnitId).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
            builder.HasData(new ProductQuantity[] {
                         new ProductQuantity
                        {
                            Id=1,
                            ProductId=1,
                            UnitId=1,
                            Quantity=1,
                            Status=Core.Enums.Status.Active,
                            InsertDate=DateTime.Now,
                            UpdateDate=DateTime.Now
                        },
                        new ProductQuantity
                        {
                            Id=2,
                            ProductId=1,
                            UnitId=1,
                            Quantity=2,
                            Status=Core.Enums.Status.Active,
                            InsertDate=DateTime.Now,
                            UpdateDate=DateTime.Now
                        }
            });
        }
    }
}
