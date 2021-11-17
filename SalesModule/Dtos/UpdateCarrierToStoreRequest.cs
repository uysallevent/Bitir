using Core.Enums;

namespace SalesModule.Dtos
{
    public class UpdateCarrierToStoreRequest
    {
        public int CarrierId { get; set; }
        public int CarrierStoreId { get; set; }
        public int? StoreId { get; set; }
        public string Plate { get; set; }
        public string DriverName { get; set; }
        public int? Capacity { get; set; }
        public Status? Status { get; set; }
    }
}
