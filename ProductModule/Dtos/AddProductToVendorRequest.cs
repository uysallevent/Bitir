using System;
using System.Collections.Generic;
using System.Text;

namespace ProductModule.Dtos
{
    public class AddProductToVendorRequest
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
