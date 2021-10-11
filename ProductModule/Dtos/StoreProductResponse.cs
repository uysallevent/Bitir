using System;
using System.Collections.Generic;
using System.Text;

namespace ProductModule.Dtos
{
    public class StoreProductResponse
    {
        public int ProductStoreId { get; set; }
        public string Name { get; set; }
        public int StoreId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
