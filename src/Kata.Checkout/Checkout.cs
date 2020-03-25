using System;
using System.Collections.Generic;
using Kata.Checkout.Models;

namespace Kata.Checkout
{
    public class Checkout : ICheckout
    {
        private Dictionary<string, Item> _scannedItems;

        public Checkout()
        {
            _scannedItems = new Dictionary<string, Item>();
        }

        public void Scan(Item item)
        {
            if(_scannedItems.ContainsKey(item.SKU))
            {
                _scannedItems[item.SKU].Quantity += item.Quantity;
            }
            else
            {
                _scannedItems.Add(item.SKU, item);
            }
        }

        public decimal Total()
        {
            var totalPrice = decimal.Zero;

            foreach (var scannedItem in _scannedItems)
            {
                totalPrice += (scannedItem.Value.UnitPrice * scannedItem.Value.Quantity);
            }

            return totalPrice;
        }

        public Dictionary<string, Item> GetScannedItems()
        {
            return _scannedItems;
        }
    }
}
