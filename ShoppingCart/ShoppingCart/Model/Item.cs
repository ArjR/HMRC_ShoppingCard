using System.Collections.Generic;

namespace ShoppingCart.Model
{
    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public List<IOffer> Offers { get; set; } // Maybe multiple offers can be applied?
    }
}