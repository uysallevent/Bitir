
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Module.Shared.Entities.AuthModuleEntities.Configuration
{
    public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
    {
        private const string tableName = "Province";
        private const string schemaName = "auth";
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
