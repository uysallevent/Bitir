using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductModule.Entities
{
    public class Product_ProductQuantity : BaseEntity
    {
        public int ProductId { get; set; }
        public int ProductQuantityId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("ProductQuantityId")]
        public ProductQuantity ProductQuantity { get; set; }
    }
}
