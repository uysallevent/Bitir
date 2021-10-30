using Core.Entities;
using Module.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Module.Shared.Entities.SalesModuleEntities
{
    public class StoreOrderViewModel: IProcedureEntity
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int UserAddressId { get; set; }
        public int DistrictId { get; set; }
        public int ProvinceId { get; set; }
        public int ProductStoreId { get; set; }
        public int ProductQuantityId { get; set; }
        public string CustomerName { get; set; }
        public string OrderProvinceName { get; set; }
        public string OrderDistrictName { get; set; }
        public string OrderAddress { get; set; }
        public int OrderQuantity { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public string OrderNote { get; set; }
        public string ProductName { get; set; }
        public decimal ProductQuantity { get; set; }
        public string ProductUnit { get; set; }
        public string ProductUnitAbbreviation { get; set; }

    }
}
