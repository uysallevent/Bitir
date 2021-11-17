using Core.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Module.Shared.Entities.SalesModuleEntities
{
    public class Carrier_Store : BaseEntity
    {
        public int CarrierId { get; set; }

        public int StoreId { get; set; }

        [ForeignKey("CarrierId")]
        public Carrier Carrier { get; set; }       
        
        [ForeignKey("StoreId")]
        public Store Store { get; set; }

        [JsonIgnore]
        [NotMapped]
        public override DateTime? InsertDate { get => base.InsertDate; set => base.InsertDate = value; }

        [JsonIgnore]
        [NotMapped]
        public override DateTime? UpdateDate { get => base.UpdateDate; set => base.UpdateDate = value; }
    }
}
