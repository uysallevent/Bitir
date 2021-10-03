using System;

namespace Bitir.Mobile.Exceptions
{
    public class UnhandledException : Exception
    {
        public UnhandledException(string message) : base(message)
        {

        }

        public UnhandledException(string message, Exception exception) : base(message, exception)
        {

        }
    }
}
