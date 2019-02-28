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
    }
}