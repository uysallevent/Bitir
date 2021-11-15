using Core.Entities;
using Core.Enums;
using Module.Shared.Entities.SalesModuleEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Module.Shared.Entities.AuthModuleEntities
{
    public class District : BaseEntity
    {
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public Province Province { get; set; }
        public IEnumerable<CarrierDistributionZone> CarrierDistributionZones { get; set; }

        [NotMapped]
        public override Status? Status { get => base.Status; set => base.Status = value; }
        [NotMapped]
        public override DateTime? InsertDate { get => base.InsertDate; set => base.InsertDate = value; }
        [NotMapped]
        public override DateTime? UpdateDate { get => base.UpdateDate; set => base.UpdateDate = value; }
    }
}
