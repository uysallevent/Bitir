using Core.Enums;

namespace ProductModule.Dtos
{
    public class UpdateProductStoreRequest
    {
        public int ProductStoreId { get; set; }
        public int ProductQuantityId { get; set; }
        public int? ProductStockId { get; set; }
        public int ProductPriceId { get; set; }
        public int? CarrierId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Status Status { get; set; }
    }
}
