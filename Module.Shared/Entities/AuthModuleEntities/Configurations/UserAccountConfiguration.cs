
using Core.Security.Hashing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Module.Shared.Entities.AuthModuleEntities.Configuration
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        private const string tableName = "UserAccount";
        private const string schemaName = "auth";
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable(tableName, schemaName).HasKey(x => x.Id);
            builder.Property(x => x.Username).IsRequired();
            builder.Ignore(x => x.Password);
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.Property(x => x.PasswordSalt).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.InsertDate).HasColumnType("datetime").HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(x => x.UpdateDate).HasColumnType("datetime").IsRequired();
            builder.HasMany(x => x.Store_UserAccounts).WithOne(x => x.UserAccount).HasForeignKey(x=>x.UserId);

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash("1234", out passwordHash, out passwordSalt);
            builder.HasData(new UserAccount[]{
                new UserAccount
                {
                    Id=1,
                    Name="test",
                    Surname="test",
                    Status=Core.Enums.Status.Active,
                    AccountTypeId=AccountTypeEnum.admin,
                    Email="t@t.com",
                    Phone="505",
                    InsertDate=DateTime.Now,
                    UpdateDate=DateTime.Now,
                    Username="admin",
                    Password="1234",
                    PasswordHash=passwordHash,
                    PasswordSalt=passwordSalt
                },
                new UserAccount
                {
                    Id=2,
                    Name="Vendor",
                    Surname="test",
                    Status=Core.Enums.Status.Active,
                    AccountTypeId=AccountTypeEnum.vendor,
                    Email="q@q.com",
                    Phone="505",
                    InsertDate=DateTime.Now,
                    UpdateDate=DateTime.Now,
                    Username="vendor",
                    Password="1234",
                    PasswordHash=passwordHash,
                    PasswordSalt=passwordSalt
                }

            });

        }
    }
}
