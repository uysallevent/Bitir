using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Module.Shared.Entities.SalesModuleEntities.Configuration
{
    public class CarrierDistributionZoneConfiguration : IEntityTypeConfiguration<CarrierDistributionZone>
    {
        private const string tableName = "CarrierDistributionZone";
        private const string schemaName = "sales";
        public virtual void Configure(EntityTypeBuilder<CarrierDistributionZone> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.CarrierId).IsRequired();
            builder.Property(x => x.ProvinceId).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
        }
    }
}
