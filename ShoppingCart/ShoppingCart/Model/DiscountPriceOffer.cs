namespace ShoppingCart.Model
{
    public class DiscountPriceOffer : IOffer
    {
        public int MinItem { get; set; }
        public decimal DiscountPrice { get; set; } // Total cost of the min items instead
    }
}