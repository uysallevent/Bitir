using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductModule.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitir.Data.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        private const string tableName = "Category";
        private const string schemaName = "product";
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
            builder.HasData(new Category[]{
                new Category
                {
                    Id=1,
                    Name="Süt Ürünleri",
                    InsertDate=DateTime.Now,
                    Status=Core.Enums.Status.Active,
                    UpdateDate=DateTime.Now,
                }
            });
        }
    }
}
