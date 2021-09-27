using System;

namespace Core.Exceptions
{
    public class OperationFailedException : Exception
    {
        public OperationFailedException(string message) : base(message)
        {

        }

        public OperationFailedException(string message, Exception ex) : base(message, ex)
        {

        }
    }
}
