using Core.Entities;
using Core.Enums;
using Module.Shared.Entities.SalesModuleEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Module.Shared.Entities.AuthModuleEntities
{
    public class Province:BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<CarrierDistributionZone> CarrierDistributionZones { get; set; }

        [JsonIgnore]
        [NotMapped]
        public override Status? Status { get => base.Status; set => base.Status = value; }
        [JsonIgnore]
        [NotMapped]
        public override DateTime? InsertDate { get => base.InsertDate; set => base.InsertDate = value; }
        [JsonIgnore]
        [NotMapped]
        public override DateTime? UpdateDate { get => base.UpdateDate; set => base.UpdateDate = value; }
    }
}
