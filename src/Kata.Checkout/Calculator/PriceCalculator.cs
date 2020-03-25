using Kata.Checkout.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.Checkout.Calculator
{
    public class PriceCalculator : ICalculator
    {
        private Dictionary<string, Offer> _specialOffers;

        public PriceCalculator()
        {
            _specialOffers = new Dictionary<string, Offer>()
            {
                {"A99", new Offer{ Quantiry = 3, OfferPrice = 1.3M} },
                {"B15", new Offer{ Quantiry = 2, OfferPrice = 0.45M} }
            };
        }

        public decimal GetItemTotal(Item item)
        {
            if(_specialOffers.ContainsKey(item.SKU))
            {
                var total = (item.Quantity / _specialOffers[item.SKU].Quantiry) * _specialOffers[item.SKU].OfferPrice;

                //any remaining items which do not fall within offer
                var remainder = item.Quantity % _specialOffers[item.SKU].Quantiry;

                if(remainder > 0)
                {
                    total += (remainder * item.UnitPrice);
                }

                return total;

            }
            else
            {
                return item.UnitPrice * item.Quantity;
            }
        }
    }
}
