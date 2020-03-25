using Kata.Checkout.Models;

namespace Kata.Checkout.Calculator
{
    public interface ICalculator
    {
        decimal GetItemTotal(Item item);
    }
}
