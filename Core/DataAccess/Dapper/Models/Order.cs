using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DataAccess.Dapper.Models
{
    public class Order
    {
        public string SortingField { get; set; } = "Id";
        public SortingType SortingType { get; set; } = SortingType.DESC;
    }

    public enum SortingType
    {
        ASC,
        DESC
    }
}
