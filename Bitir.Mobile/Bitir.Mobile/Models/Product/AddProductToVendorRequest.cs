﻿namespace Bitir.Mobile.Models.Product
{
    public class AddProductToVendorRequest
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
