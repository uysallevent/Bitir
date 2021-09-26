using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductModule.Entities
{
    public class ProductPrice:BaseEntity
    {
        public int ProductId { get; set; }
        public int VendorId { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
