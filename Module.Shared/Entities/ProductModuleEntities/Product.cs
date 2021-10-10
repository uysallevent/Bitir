using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Shared.Entities.ProductModuleEntities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public IEnumerable<Product_Store> ProductStores { get; set; }

        public IEnumerable<ProductQuantity> ProductQuantity { get; set; }
    }
}
