using System;

namespace Bitir.Mobile.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException(string msg) : base(msg)
        {

        }

        public ServiceException(string msg, Exception exception) : base(msg, exception)
        {

        }
    }
}
