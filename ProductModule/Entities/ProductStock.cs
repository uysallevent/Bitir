using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductModule.Entities
{
    public class ProductStock : BaseEntity
    {
        public int ProductId { get; set; }
        public int MerchantId { get; set; }
        public decimal Quantity { get; set; }
    }
}
