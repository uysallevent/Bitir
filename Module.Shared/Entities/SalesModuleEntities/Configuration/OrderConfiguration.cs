using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Module.Shared.Entities.SalesModuleEntities.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        private const string tableName = "Order";
        private const string schemaName = "sales";
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.ProductStoreId).IsRequired();
            builder.Property(x => x.OrderStatus).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
        }
    }
}
