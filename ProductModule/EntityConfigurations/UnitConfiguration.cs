using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductModule.Entities;
using System;

namespace ProductModule.EntityConfigurations
{
    public class UnitConfiguration : IEntityTypeConfiguration<Unit>
    {
        private const string tableName = "Unit";
        private const string schemaName = "product";
        public void Configure(EntityTypeBuilder<Unit> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
            builder.HasData(new Unit[] {
                new Unit
                {
                    Id=1,
                    Name="Litre",
                    Abbreviation="lt",
                    Status=Core.Enums.Status.Active,
                    InsertDate=DateTime.Now,
                    UpdateDate=DateTime.Now
                }
            });
        }
    }
}
