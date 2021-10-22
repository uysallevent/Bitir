using Core.Enums;

namespace SalesModule.Dtos
{
    public class StoreCarrier
    {
        public int Id { get; set; }
        public int CarrierId { get; set; }
        public int CarrierStoreId { get; set; }
        public int? Capacity { get; set; }
        public string Plate { get; set; }
        public Status? Status { get; set; }
    }
}
