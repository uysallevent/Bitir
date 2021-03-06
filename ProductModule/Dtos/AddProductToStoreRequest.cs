using Core.Enums;

namespace ProductModule.Dtos
{
    public class AddProductToStoreRequest
    {
        public int ProductQuantityId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Status Status { get; set; }
    }
}
