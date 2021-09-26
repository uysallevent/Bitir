using Core.Entities;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AuthModule.Entities
{
    public class AccountType:BaseEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public IEnumerable<UserAccount> UserAccounts { get; set; }
    }
}
