using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataAccess.Dapper.Models
{
    public class Paging
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int PageOfset { get { return (PageNumber - 1) * PageSize; } }
    }
}
