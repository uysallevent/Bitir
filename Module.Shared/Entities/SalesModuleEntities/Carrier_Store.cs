using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
