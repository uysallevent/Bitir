using AuthModule.Entities;
using AuthModule.Security.Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bitir.Data.EntityConfigurations
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        private const string tableName = "UserAccount";
        private const string schemaName = "auth";
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.PasswordSalt).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("1234", out passwordHash, out passwordSalt);
            builder.HasData(new UserAccount[]{
                new UserAccount
                {
                    Id=1,
                    Name="test",
                    Surname="test",
                    Status=1,
                    AccountTypeId=1,
                     Email="t@t.com",
                     Phone="505",
                     InsertDate=DateTime.Now,
                     UpdateDate=DateTime.Now,
                     Username="admin",
                     Password="1234",
                     PasswordHash=passwordHash,
                     PasswordSalt=passwordSalt
                }

            });

        }
    }
}
