
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Module.Shared.Entities.AuthModuleEntities.Configuration
{
    public class NeighbourhoodConfiguration : IEntityTypeConfiguration<Neighbourhood>
    {
        private const string tableName = "Neighbourhood";
        private const string schemaName = "auth";
        public void Configure(EntityTypeBuilder<Neighbourhood> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.LocalityName).IsRequired();
            builder.Property(x => x.DistrictId).IsRequired();
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
