using Core.Enums;
using System.Collections.Generic;

namespace SalesModule.Dtos
{
    public class UpdateCarrierToStoreRequest
    {
        public int? CarrierDistributionZoneId { get; set; }
        public int CarrierId { get; set; }
        public int CarrierStoreId { get; set; }
        public int? StoreId { get; set; }
        public int ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public List<string> LocalityNames { get; set; }
        public string Plate { get; set; }
        public string DriverName { get; set; }
        public int? Capacity { get; set; }
        public Status? Status { get; set; }
    }
}
