using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Shared.Entities.ProductModuleEntities
{
    public class ProductQuantity : BaseEntity
    {
        public decimal Quantity { get; set; }
        public int ProductId { get; set; }
        public int UnitId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }
    }
}
