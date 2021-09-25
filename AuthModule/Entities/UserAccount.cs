using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AuthModule.Entities
{
    public class UserAccount : IEntity
    {
        public int Id { get; set; }
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
        [JsonIgnore]
        public DateTime? InsertDate { get; set; }
        [JsonIgnore]
        public ICollection<UserToken> UserTokens { get; set; }
    }
}