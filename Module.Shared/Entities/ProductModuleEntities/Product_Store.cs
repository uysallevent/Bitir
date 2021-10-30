using Core.Entities;
using Module.Shared.Entities.SalesModuleEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Module.Shared.Entities.ProductModuleEntities
{
    public class Product_Store : BaseEntity
    {
        public int ProductQuantityId { get; set; }
        public int StoreId { get; set; }

        [ForeignKey("ProductQuantityId")]
        public ProductQuantity ProductQuantity { get; set; }

        [ForeignKey("StoreId")]
        public Store Store { get; set; }

        public IEnumerable<ProductStorePrice> ProductStorePrices { get; set; }
        public IEnumerable<Order> Order { get; set; }

        [JsonIgnore]
        [NotMapped]
        public override DateTime? InsertDate { get => base.InsertDate; set => base.InsertDate = value; }

        [JsonIgnore]
        [NotMapped]
        public override DateTime? UpdateDate { get => base.UpdateDate; set => base.UpdateDate = value; }

    }
}
