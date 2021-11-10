using Module.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Bitir.Mobile.Models.Order
{
    public class StoreOrder
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public int? CarrierId { get; set; }
        public string CustomerName { get; set; }
        public string OrderProvinceName { get; set; }
        public string OrderDistrictName { get; set; }
        public string ProvinceDistrict { get { return $"{OrderProvinceName}-{OrderDistrictName}"; } }
        public string OrderAddress { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }
        public string OrderNote { get; set; }
        public DateTime? OrderDate { get; set; }
        public List<StoreOrderDetail> StoreOrderDetails { get; set; }
    }
}
