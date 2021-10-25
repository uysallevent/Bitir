using Core.Enums;

namespace ProductModule.Dtos
{
    public class StoreProdByCarrierResponse
    {
        public int ProductStockId { get; set; }
        public int ProductStoreId { get; set; }
        public string ProductName { get; set; }
        public string UnitName { get; set; }
        public string Abbreviation { get; set; }
        public int Capacity { get; set; }
        public int ProductStock { get; set; }
        public decimal Quantity { get; set; }
        public string FullName { get { return $"{ProductName} {Quantity} {Abbreviation}"; } }
    }
}
