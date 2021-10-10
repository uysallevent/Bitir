using Core.Entities;
using Module.Shared.Entities.SalesModuleEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Shared.Entities.ProductModuleEntities
{
    public class ProductStock : BaseEntity
    {
        public int ProductStoreId { get; set; }
        public int? CarrierId { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ProductStoreId")]
        public Product_Store ProductStore { get; set; }
        [ForeignKey("CarrierId")]
        public Carrier Carrier { get; set; }
    }
}
