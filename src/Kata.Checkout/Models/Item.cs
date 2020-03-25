using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.Checkout.Models
{
    public class Item
    {
        public Item()
        {
            Quantity = 1;
        }

        public string SKU { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; internal set; }
    }
}
