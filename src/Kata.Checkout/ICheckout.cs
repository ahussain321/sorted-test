using Kata.Checkout.Models;

namespace Kata.Checkout
{
    public interface ICheckout
    {
        void Scan(Item item);

        decimal Total();
    }
}
