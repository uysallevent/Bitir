using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Module.Shared.Entities.SalesModuleEntities.Configuration
{
    public class Store_UserAccountConfiguration : IEntityTypeConfiguration<Store_UserAccount>
    {
        private const string tableName = "Store_UserAccount";
        private const string schemaName = "sales";
        public virtual void Configure(EntityTypeBuilder<Store_UserAccount> builder)
        {
            builder.ToTable(tableName, schemaName);//.HasKey(x => x.Id);
            builder.HasKey(e => new { e.UserId, e.StoreId });
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
            builder.HasOne(x => x.UserAccount).WithMany(x => x.Store_UserAccounts).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Store).WithMany(x => x.Store_UserAccounts).HasForeignKey(x => x.StoreId);
        }
    }
}
