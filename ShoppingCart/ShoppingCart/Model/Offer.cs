namespace ShoppingCart.Model
{
    public class Offer
    {
        public int MinItem { get; set; }
        public int? AddItem { get; set; }
        public decimal? DiscountPrice { get; set; } // Total cost of the min items instead
    }
}