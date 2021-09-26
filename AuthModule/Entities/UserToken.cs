using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthModule.Entities
{
    public class UserToken : BaseEntity
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpirationDate { get; set; }

        [ForeignKey("UserId")]
        public UserAccount UserAccount { get; set; }
    }
}