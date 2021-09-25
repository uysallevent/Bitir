using System;
using System.Collections.Generic;
using System.Text;

namespace Bitir.Data.Model.Dtos
{
    public class ResponseWrapperListing<T> : ResponseWrapper<T> where T : class
    {
        public ResponseWrapperListing(IEnumerable<T> tentities, int total=0, int page=0, T model = null) : base(model)
        {
            Total = total;
            Page = page;
            TEntities = tentities;
        }

        public int Total { get; set; }
        public int Page { get; set; }
        public IEnumerable<T> TEntities { get; set; }
    }
}
