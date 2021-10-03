using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductModule.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public IEnumerable<ProductPrice> ProductPrices { get; set; }

        public IEnumerable<ProductStock> ProductStocks { get; set; }

        public IEnumerable<Product_ProductQuantity> Product_ProductQuantity { get; set; }
    }
}
