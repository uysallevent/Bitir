
using Core.Entities;
using Module.Shared.Entities.AuthModuleEntities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Module.Shared.Entities.SalesModuleEntities
{
    public class Store_UserAccount: BaseEntity
    {
        public int UserId { get; set; }
        public int StoreId { get; set; }

        [JsonIgnore]
        [NotMapped]
        public override DateTime? InsertDate { get => base.InsertDate; set => base.InsertDate = value; }

        [JsonIgnore]
        [NotMapped]
        public override DateTime? UpdateDate { get => base.UpdateDate; set => base.UpdateDate = value; }

        [ForeignKey("UserId")]
        public UserAccount UserAccount { get; set; }       
        
        [ForeignKey("StoreId")]
        public Store Store { get; set; }
    }
}
