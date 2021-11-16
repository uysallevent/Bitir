using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesModule.Dtos
{
    public class CarrierZoneRequest
    {
        public int CarrierId { get; set; }
        public int ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public List<int?> NeighborhoodIds { get; set; }
        public Status? Status { get; set; }
    }
}
