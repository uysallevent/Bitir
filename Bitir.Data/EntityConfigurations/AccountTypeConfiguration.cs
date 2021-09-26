using AuthModule.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Bitir.Data.EntityConfigurations
{
    public class AccountTypeConfiguration : IEntityTypeConfiguration<AccountType>
    {
        private const string tableName = "AccountType";
        private const string schemaName = "auth";
        public void Configure(EntityTypeBuilder<AccountType> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
            builder.HasData(new AccountType[] {
                new AccountType
                {
                    Id=1,
                    InsertDate=DateTime.Now,
                    UpdateDate=DateTime.Now,
                    Name="Admin",
                    Status=1
                },
                new AccountType
                {
                    Id=2,
                    InsertDate=DateTime.Now,
                    UpdateDate=DateTime.Now,
                    Name="Customer",
                    Status=1
                }
                ,
                new AccountType
                {
                    Id=3,
                    InsertDate=DateTime.Now,
                    UpdateDate=DateTime.Now,
                    Name="Vendor",
                    Status=1
                }

            });
        }

    }
}
