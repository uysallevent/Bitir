using Core.Entities;
using Module.Shared.Entities.SalesModuleEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Module.Shared.Entities.AuthModuleEntities
{
    public class UserAccount : BaseEntity
    {
        public string Username { get; set; }
        [NotMapped]
        public string Password { get; set; }
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public AccountTypeEnum AccountTypeId { get; set; }
        [JsonIgnore]
        public IEnumerable<UserToken> UserTokens { get; set; }
        [JsonIgnore]
        public IEnumerable<Store_UserAccount> Store_UserAccounts { get; set; }
        [JsonIgnore]
        public IEnumerable<Order> Order { get; set; }
        [JsonIgnore]
        public IEnumerable<UserAddress> UserAddress { get; set; }
    }
}