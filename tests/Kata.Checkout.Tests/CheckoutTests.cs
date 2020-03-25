using Kata.Checkout.Calculator;
using Kata.Checkout.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kata.Checkout.Tests
{
    [TestClass]
    public class CheckoutTests
    {
        [TestMethod]
        public void GivenScan_WhenIScanAnItem_ThenItemScanned()
        {

            var checkout = new Checkout(new PriceCalculator());

            var item1 = new Item()
            {
                SKU = "",
                UnitPrice = 0.5M
            };

            checkout.Scan(item1);

            var scannedItems = checkout.GetScannedItems();

            Assert.IsTrue(scannedItems.ContainsKey(item1.SKU));

            Assert.AreEqual(item1.UnitPrice, scannedItems[item1.SKU].UnitPrice);

            Assert.AreEqual(1, scannedItems[item1.SKU].Quantity);
            
        }

        [TestMethod]
        public void GivenScan_WhenIScanMultipleItemsOfSameSKU_ThenScannedItemQuantityCorect()
        {

            var checkout = new Checkout(new PriceCalculator());

            var item1 = new Item()
            {
                SKU = "A99",
                UnitPrice = 0.5M
            };

            checkout.Scan(item1);

            var item2 = new Item()
            {
                SKU = "A99",
                UnitPrice = 0.5M
            };

            checkout.Scan(item2);

            var scannedItems = checkout.GetScannedItems();

            Assert.IsTrue(scannedItems.ContainsKey(item1.SKU));

            Assert.AreEqual(item1.UnitPrice, scannedItems[item1.SKU].UnitPrice);

            Assert.AreEqual(2, scannedItems[item1.SKU].Quantity);

        }

        [TestMethod]
        public void GivenTotal_WhenIScanMultipleItems_ThenCorrectTotalPriceReturned()
        {

            var checkout = new Checkout(new PriceCalculator());

            checkout.Scan(new Item { SKU = "A99", UnitPrice = 0.5M});
            checkout.Scan(new Item { SKU = "B15", UnitPrice = 0.3M });
            checkout.Scan(new Item { SKU = "C40", UnitPrice = 0.6M });

            var total = checkout.Total();

            var expectedTotal = 0.5M + 0.3M + 0.6M;

            Assert.AreEqual(expectedTotal, total);
        }

        [TestMethod]
        public void GivenTotal_WhenIScanA99WithOfferQuantity_ThenSpecialOfferApplied()
        {

            var checkout = new Checkout(new PriceCalculator());

            checkout.Scan(new Item { SKU = "A99", UnitPrice = 0.5M });
            checkout.Scan(new Item { SKU = "A99", UnitPrice = 0.5M });
            checkout.Scan(new Item { SKU = "A99", UnitPrice = 0.5M });

            var total = checkout.Total();

            Assert.AreEqual(1.3M, total);
        }

        [TestMethod]
        public void GivenTotal_WhenIScanB15WithOfferQuantity_ThenSpecialOfferApplied()
        {

            var checkout = new Checkout(new PriceCalculator());

            checkout.Scan(new Item { SKU = "B15", UnitPrice = 0.3M });
            checkout.Scan(new Item { SKU = "B15", UnitPrice = 0.3M });
            checkout.Scan(new Item { SKU = "B15", UnitPrice = 0.3M });

            var total = checkout.Total();

            var expectedTotal = 0.45M + 0.3M; // 3rd item not within offer

            Assert.AreEqual(expectedTotal, total);
        }

        [TestMethod]
        public void GivenTotal_WhenIScanOfferItemsInUnorder_ThenSpecialOfferApplied()
        {

            var checkout = new Checkout(new PriceCalculator());

            checkout.Scan(new Item { SKU = "B15", UnitPrice = 0.3M });
            checkout.Scan(new Item { SKU = "A99", UnitPrice = 0.5M });
            checkout.Scan(new Item { SKU = "B15", UnitPrice = 0.3M });

            var total = checkout.Total();

            var expectedTotal = 0.45M + 0.5M; // 2 B15s at offer price and A99 at normal price

            Assert.AreEqual(expectedTotal, total);
        }
    }
}
