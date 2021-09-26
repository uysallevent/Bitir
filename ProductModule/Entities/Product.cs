using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductModule.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public int UnitId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }

        public IEnumerable<ProductPrice> ProductPrices { get; set; }

        public IEnumerable<ProductStock> ProductStocks { get; set; }
    }
}
