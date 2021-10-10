using System;

namespace Core.Exceptions
{
    public class ClaimExpection : Exception
    {
        public ClaimExpection(string message) : base(message)
        {

        }
        public ClaimExpection(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
