using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataAccess.Dapper.Models
{
    public class PagingOrder
    {
        public Paging Paging { get; set; }
        public Order Order { get; set; }
    }
}
