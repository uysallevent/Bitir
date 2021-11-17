using System;
using System.Collections.Generic;
using System.Text;

namespace SalesModule.Dtos
{
    public class AddCarrierToStoreRequest
    {
        public int? StoreId { get; set; }
        public string Plate { get; set; }
        public string DriverName { get; set; }
        public int? Capacity { get; set; }
    }
}
