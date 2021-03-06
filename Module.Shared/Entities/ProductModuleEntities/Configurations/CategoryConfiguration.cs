using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Module.Shared.Entities.ProductModuleEntities.Configuration
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
            builder.HasMany(x => x.Products).WithOne(x => x.Category).HasForeignKey(x => x.CategoryId);
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
