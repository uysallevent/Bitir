using System;
using System.Collections.Generic;
using System.Text;

namespace Bitir.Mobile.Models.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
