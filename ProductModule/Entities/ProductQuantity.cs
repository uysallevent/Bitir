using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductModule.Entities
{
    public class ProductQuantity : BaseEntity
    {
        public double Quantity { get; set; }
        public int UnitId { get; set; }
        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }
        public IEnumerable<Product_ProductQuantity> Product_ProductQuantity { get; set; }

    }
}
