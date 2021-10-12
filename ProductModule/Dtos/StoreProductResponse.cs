using System;
using System.Collections.Generic;
using System.Text;

namespace ProductModule.Dtos
{
    public class StoreProductResponse
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int Stock { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public string Abbreviation { get; set; }
    }
}
