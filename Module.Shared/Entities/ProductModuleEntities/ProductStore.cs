using Core.Entities;
using Module.Shared.Entities.SalesModuleEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Shared.Entities.ProductModuleEntities
{
    public class ProductStore : BaseEntity
    {
        public int ProductId { get; set; }
        public int StoreId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [ForeignKey("StoreId")]
        public Store Store { get; set; }

        public IEnumerable<ProductPrice> ProductPrices { get; set; }

        public IEnumerable<ProductStock> ProductStocks { get; set; }

    }
}
