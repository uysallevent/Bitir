
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Module.Shared.Entities.AuthModuleEntities.Configuration
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        private const string tableName = "District";
        private const string schemaName = "auth";
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.ProvinceId).IsRequired();
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
