using Core.Entities;
using Module.Shared.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Module.Shared.Entities.SalesModuleEntities
{
    [Table("StoreOrdersView", Schema = "sales")]
    public class StoreOrdersView : IEntity
    {
        [Key]
        public int OrderId { get; set; }
        public int StoreId { get; set; }
        public int UserId { get; set; }
        public int UserAddressId { get; set; }
        public int DistrictId { get; set; }
        public int ProvinceId { get; set; }
        public int ProductStoreId { get; set; }
        public int ProductQuantityId { get; set; }
        public int? CarrierId { get; set; }
        public string CustomerName { get; set; }
        public string OrderProvinceName { get; set; }
        public string OrderDistrictName { get; set; }
        public string OrderAddress { get; set; }
        public int OrderQuantity { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public string OrderNote { get; set; }
        public DateTime? OrderDate { get; set; }
        public string ProductName { get; set; }
        public decimal ProductQuantity { get; set; }
        public string ProductUnit { get; set; }
        public string ProductUnitAbbreviation { get; set; }
    }
}
