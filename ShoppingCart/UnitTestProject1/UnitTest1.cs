using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCart.Implementer;
using ShoppingCart.Model;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        public Checkout CreateAppleOrangeCheckoutWithoutOffers()
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

        public Checkout CreateAppleOrangeCheckoutWithOffers()
        {
            Item apple = new Item();
            apple.ID = 1;
            apple.Name = "Apple";
            apple.Price = 0.60M;

            var appleOffer = new AddItemOffer();
            appleOffer.MinItem = 1;
            appleOffer.AddItem = 1;

            apple.Offer = appleOffer;

            Item orange = new Item();
            orange.ID = 2;
            orange.Name = "Orange";
            orange.Price = 0.25M;

            var orangeOffer = new DiscountPriceOffer();
            orangeOffer.MinItem = 3;
            orangeOffer.DiscountPrice = 2 * orange.Price;

            orange.Offer = orangeOffer;

            var checkout = new Checkout(new List<Item>() { apple, orange });

            return checkout;
        }

        [TestMethod]
        public void MainNameTest()
        {
            var checkout = CreateAppleOrangeCheckoutWithoutOffers();
            var success = checkout.ScanItems("Apple, Apple, Orange, Apple");
            var total = checkout.CalculateTotal();

            Assert.IsTrue(success);
            Assert.IsTrue(total == 2.05M);
        }

        [TestMethod]
        public void MainIdTest()
        {
            var checkout = CreateAppleOrangeCheckoutWithoutOffers();
            var success = checkout.ScanItems("1, 1, 2, 1");
            var total = checkout.CalculateTotal();

            Assert.IsTrue(success);
            Assert.IsTrue(total == 2.05M);
        }

        [TestMethod]
        public void SingleItemTest1()
        {
            var checkout = CreateAppleOrangeCheckoutWithoutOffers();
            var success = checkout.ScanItems("Apple");
            var total = checkout.CalculateTotal();

            Assert.IsTrue(success);
            Assert.IsTrue(total == 0.60M);
        }

        [TestMethod]
        public void SingleItemTest2()
        {
            var checkout = CreateAppleOrangeCheckoutWithoutOffers();
            var success = checkout.ScanItems("Orange");
            var total = checkout.CalculateTotal();

            Assert.IsTrue(success);
            Assert.IsTrue(total == 0.25M);
        }

        [TestMethod]
        public void BadItemTest1()
        {
            var checkout = CreateAppleOrangeCheckoutWithoutOffers();
            var success = checkout.ScanItems("Orange, Bin");
            var total = checkout.CalculateTotal();

            Assert.IsFalse(success);
        }

        [TestMethod]
        public void BadItemTest2()
        {
            var checkout = CreateAppleOrangeCheckoutWithoutOffers();
            var success = checkout.ScanItems("Jack");
            var total = checkout.CalculateTotal();

            Assert.IsFalse(success);
        }

        [TestMethod]
        public void OfferMainNameTest()
        {
            var checkout = CreateAppleOrangeCheckoutWithOffers();
            var success = checkout.ScanItems("Apple, Apple, Apple, Apple, Orange, Orange, Orange");
            var total = checkout.CalculateTotal();

            Assert.IsTrue(success);
            Assert.IsTrue(total == (0.6M + 0.6M + 0.25M + 0.25M));
        }

        [TestMethod]
        public void OfferMainNameTest2()
        {
            var checkout = CreateAppleOrangeCheckoutWithOffers();
            var success = checkout.ScanItems("Apple, Apple, Apple, Apple, Apple, Orange, Orange, Orange, Orange");
            var total = checkout.CalculateTotal();

            Assert.IsTrue(success);
            Assert.IsTrue(total == (0.6M + 0.6M + 0.6M + 0.25M + 0.25M + 0.25M));
        }

        [TestMethod]
        public void OfferMainNameTest3()
        {
            var checkout = CreateAppleOrangeCheckoutWithOffers();
            var success = checkout.ScanItems("Apple, Orange");
            var total = checkout.CalculateTotal();

            Assert.IsTrue(success);
            Assert.IsTrue(total == (0.6M + 0.25M));
        }
    }
}