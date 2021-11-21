using Core.Entities;
using Core.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Shared.Entities.SalesModuleEntities
{
    [Table("StoreCarriersView", Schema = "sales")]
    public class StoreCarriersView : IEntity
    {
        public int? CarrierDistributionZoneId { get; set; }
        [Key]
        public int CarrierId { get; set; }
        public int? StoreId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? NeighbourhoodId { get; set; }
        [NotMapped]
        public List<int?> NeighbourhoodIds { get; set; }
        public string DriverName { get; set; }
        public string Plate { get; set; }
        public int Capacity { get; set; }
        public string ProvinceName { get; set; }
        public string DistrictName { get; set; }
        public string NeigbourhoodName { get; set; }
        public string LocalityName { get; set; }
        public Status CarrierStatus { get; set; }
    }
}
