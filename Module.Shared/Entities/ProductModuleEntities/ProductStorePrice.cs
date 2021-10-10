using Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Shared.Entities.ProductModuleEntities
{
    public class ProductStorePrice:BaseEntity
    {
        public int ProductStoreId { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("ProductStoreId")]
        public Product_Store ProductStore { get; set; }
    }
}
