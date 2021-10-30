using System.Collections.Generic;

namespace Core.Wrappers
{
    public class ResponseWrapperListing<T> : ResponseWrapper<T> where T : class
    {
        public ResponseWrapperListing(IEnumerable<T> tentities, int total = 0, int page = 0, T model = null) : base(model)
        {
            Total = total;
            Page = page;
            List = tentities;
        }
        public ResponseWrapperListing()
        {

        }

        public int Total { get; set; }
        public int Page { get; set; }
        public IEnumerable<T> List { get; set; }
    }
}
