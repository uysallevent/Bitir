using Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace Module.Shared.Entities.ProductModuleEntities
{
    public class StoreProductByStoreViewModel : IEntity
    {
        [Key]
        public int ProductStockId { get; set; }
        public int ProductStoreId { get; set; }
        public string ProductName { get; set; }
        public string UnitName { get; set; }
        public string Abbreviation { get; set; }
        public string Plate { get; set; }
        public int? Capacity { get; set; }
        public int ProductStock { get; set; }
        public decimal Quantity { get; set; }
    }
}
