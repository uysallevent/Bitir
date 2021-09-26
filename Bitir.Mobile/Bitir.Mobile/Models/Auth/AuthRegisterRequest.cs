using System;
using System.Collections.Generic;
using System.Text;

namespace Bitir.Mobile.Models.Auth
{
    public class AuthRegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int AccountTypeId { get; set; }
    }
}
