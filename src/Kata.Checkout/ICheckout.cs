using Kata.Checkout.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.Checkout
{
    public interface ICheckout
    {
        void Scan(Item item);
    }
}
