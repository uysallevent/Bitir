﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Module.Shared.Entities.SalesModuleEntities.Configuration
{
    public class Carrier_StoreConfiguration : IEntityTypeConfiguration<Carrier_Store>
    {
        private const string tableName = "Carrier_Store";
        private const string schemaName = "sales";
        public virtual void Configure(EntityTypeBuilder<Carrier_Store> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.StoreId).IsRequired();
            builder.Property(x => x.CarrierId).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
        }
    }
}
