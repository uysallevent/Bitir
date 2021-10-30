using System;

namespace Core.Wrappers
{
    public class ResponseWrapper<T>
    {
        public ResponseWrapper()
        {

        }

        public ResponseWrapper(T result)
        {
            Result = result;
        }
        public virtual T Result { get; set; }
    }
}
