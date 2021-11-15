using Core.Entities;
using Module.Shared.Entities.AuthModuleEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Shared.Entities.SalesModuleEntities
{
    public class CarrierDistributionZone:BaseEntity
    {
        public int CarrierId { get; set; }
        public int ProvinceId { get; set; }
        public int? DistrictId { get; set; }
        public int? NeighbourhoodId { get; set; }

        [ForeignKey("CarrierId")]
        public Carrier Carrier { get; set; }

        [ForeignKey("ProvinceId")]
        public Province Province { get; set; }

        [ForeignKey("DistrictId")]
        public District District { get; set; }

        [ForeignKey("NeighbourhoodId")]
        public Neighbourhood Neighbourhood { get; set; }
    }
}
