using Core.Entities;
using Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Module.Shared.Entities.ProductModuleEntities
{
    public class StoreProductViewModel:IEntity
    {
        public int Id { get; set; }
        public int ProductStoreId { get; set; }
        [Key]
        public int ProductStockId { get; set; }
        public int ProductPriceId { get; set; }
        public int Stock { get; set; }
        public string Name { get; set; }
        public string FullName { get { return $"{Name} {Quantity} {Abbreviation}"; } }
        public string FullNameWithCapacity { get { return $"{Name} {Quantity} {Abbreviation} (Toplam Stok: {Stock} adet)"; } }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public string Abbreviation { get; set; }
        public string Position { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }
    }
}
