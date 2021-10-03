using Bitir.Mobile.Models.Common;
using System;

namespace Bitir.Mobile.Exceptions
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string message) : base(message)
        {

        }       
        
        public InternalServerErrorException(ErrorResponse errorResponse) : base(errorResponse.Message)
        {

        }

        public InternalServerErrorException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
