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

            var checkout = new Checkout();

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
        public void GivenScan_WhenIScanMultipleItemsOfSameSKU_ThenItemQuantityCorect()
        {

            var checkout = new Checkout();

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
    }
}
