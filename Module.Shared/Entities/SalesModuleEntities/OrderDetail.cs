using Core.Entities;
using Module.Shared.Entities.ProductModuleEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Shared.Entities.SalesModuleEntities
{
    public class OrderDetail : BaseEntity
    {
        public int OrderId { get; set; }

        public int ProductStoreId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("ProductStoreId")]
        public Product_Store Product_Store { get; set; }
    }
}
