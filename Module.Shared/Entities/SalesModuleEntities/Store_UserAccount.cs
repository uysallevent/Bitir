
using Core.Entities;
using Module.Shared.Entities.AuthModuleEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Shared.Entities.SalesModuleEntities
{
    public class Store_UserAccount:BaseEntity
    {
        public int UserId { get; set; }
        public int StoreId { get; set; }

        [ForeignKey("UserId")]
        public UserAccount UserAccount { get; set; }       
        
        [ForeignKey("StoreId")]
        public Store Store { get; set; }
    }
}
