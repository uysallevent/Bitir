using System;
using System.Collections.Generic;
using System.Text;

namespace Bitir.Mobile.Models.Order
{
    public class StoreOrderDetail
    {
        public int ProductStoreId { get; set; }
        public int OrderQuantity { get; set; }
        public string FullName { get { return $"{ProductName} {ProductQuantity} {ProductUnitAbbreviation}"; } }
        public string ProductName { get; set; }
        public decimal ProductQuantity { get; set; }
        public string ProductUnit { get; set; }
        public string ProductUnitAbbreviation { get; set; }
    }
}
