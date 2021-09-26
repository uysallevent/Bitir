using System;

namespace Bitir.Data.Model.Dtos
{
    public class ResponseWrapper<T>
    {
        public ResponseWrapper(T result)
        {
            Result = result;
        }
        public virtual T Result { get; set; }
    }
}
