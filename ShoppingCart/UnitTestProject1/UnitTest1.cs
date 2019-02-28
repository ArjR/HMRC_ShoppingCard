using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart.Implementer;
using ShoppingCart.Model;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public Checkout CreateAppleOrangeCheckout()
        {
            Item apple = new Item();
            apple.ID = 1;
            apple.Name = "Apple";
            apple.Price = 0.60M;

            Item orange = new Item();
            orange.ID = 2;
            orange.Name = "Orange";
            orange.Price = 0.25M;

            var checkout = new Checkout(new List<Item>() { apple, orange });

            return checkout;
        }

        [TestMethod]
        public void MainNameTest()
        {
            var checkout = CreateAppleOrangeCheckout();
            var success = checkout.ScanItems("Apple, Apple, Orange, Apple");
            var total = checkout.CalculateTotal();

            Assert.IsTrue(success);
            Assert.IsTrue(total == 2.05M);
        }

        [TestMethod]
        public void MainIdTest()
        {
            var checkout = CreateAppleOrangeCheckout();
            var success = checkout.ScanItems("1, 1, 2, 1");
            var total = checkout.CalculateTotal();

            Assert.IsTrue(success);
            Assert.IsTrue(total == 2.05M);
        }

        [TestMethod]
        public void SingleItemTest1()
        {
            var checkout = CreateAppleOrangeCheckout();
            var success = checkout.ScanItems("Apple");
            var total = checkout.CalculateTotal();

            Assert.IsTrue(success);
            Assert.IsTrue(total == 0.60M);
        }

        [TestMethod]
        public void SingleItemTest2()
        {
            var checkout = CreateAppleOrangeCheckout();
            var success = checkout.ScanItems("Orange");
            var total = checkout.CalculateTotal();

            Assert.IsTrue(success);
            Assert.IsTrue(total == 0.25M);
        }

        [TestMethod]
        public void BadItemTest1()
        {
            var checkout = CreateAppleOrangeCheckout();
            var success = checkout.ScanItems("Orange, Bin");
            var total = checkout.CalculateTotal();

            Assert.IsFalse(success);
        }

        [TestMethod]
        public void BadItemTest2()
        {
            var checkout = CreateAppleOrangeCheckout();
            var success = checkout.ScanItems("Jack");
            var total = checkout.CalculateTotal();

            Assert.IsFalse(success);
        }
    }
}