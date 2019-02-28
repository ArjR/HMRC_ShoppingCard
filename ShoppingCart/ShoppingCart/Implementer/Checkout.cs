using ShoppingCart.Model;
using System.Collections.Generic;

namespace ShoppingCart.Implementer
{
    public class Checkout
    {
        private List<Item> itemsForSale = new List<Item>();
        private Dictionary<Item, int> scannedItems = new Dictionary<Item, int>();

        public Checkout(List<Item> itemsForSale)
        {
            this.itemsForSale = itemsForSale;
        }
    }
}