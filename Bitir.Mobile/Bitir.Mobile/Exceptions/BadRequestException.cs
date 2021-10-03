using Bitir.Mobile.Models.Common;
using System;

namespace Bitir.Mobile.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {

        }

        public BadRequestException(ErrorResponse errorResponse) : base(errorResponse.Message)
        {

        }

        public BadRequestException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
