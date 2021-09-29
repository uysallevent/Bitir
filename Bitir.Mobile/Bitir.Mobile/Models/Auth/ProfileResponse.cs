using System;
using System.Collections.Generic;
using System.Text;

namespace Bitir.Mobile.Models.Auth
{
    public class ProfileResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get { return $"{Name} {Surname}"; } }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? AccountTypeId { get; set; }
    }
}
