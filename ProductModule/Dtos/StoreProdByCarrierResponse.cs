using Core.Enums;

namespace ProductModule.Dtos
{
    public class StoreProdByCarrierResponse
    {
        public int CarrierId { get; set; }
        public int ProductStockId { get; set; }
        public int ProductStock { get; set; }
        public string Plate { get; set; }
        public Status? Status { get; set; }
    }
}
