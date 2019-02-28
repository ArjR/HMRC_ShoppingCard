using ShoppingCart.Model;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCart.Implementer
{
    public class Checkout
    {
        private List<Item> _itemsForSale = new List<Item>();
        private Dictionary<Item, int> _scannedItems = new Dictionary<Item, int>();

        public Checkout(List<Item> itemsForSale)
        {
            _itemsForSale = itemsForSale;
        }

        public Item IdentifyItem(string itemName)
        {
            itemName = itemName.Trim(); // Remove unncessary whitespace
            return _itemsForSale.FirstOrDefault(x => x.Name.ToLower() == itemName.ToLower() || x.ID.ToString() == itemName);
        }

        public bool ScanItem(string itemName)
        {
            var item = IdentifyItem(itemName);

            if (item == null)
            {
                // Item not found
                return false;
            }
            else
            {
                // Ensure item is part of scanned items
                if (!_scannedItems.ContainsKey(item))
                {
                    _scannedItems.Add(item, 0);
                }

                // Add Item to Scanned Items
                _scannedItems[item]++;

                return true;
            }
        }

        public bool ScanItems(string itemList)
        {
            var itemStrings = itemList.Split(','); // May need to add extra characters if not comma seperated???

            // Scan each item
            foreach (var itemString in itemStrings)
            {
                var success = ScanItem(itemString);

                if (!success) return false;
            }

            return true;
        }

        public decimal CalculateTotal()
        {
            var totalPrice = 0M;

            foreach (var itemType in _scannedItems)
            {
                var offer = itemType.Key.Offer;

                // Take into account offers first
                if (offer == null)
                {
                    totalPrice += itemType.Value * itemType.Key.Price;
                }
                else
                {
                    var preOfferItemCount = itemType.Value;
                    var postOfferItemCount = 0;

                    if (offer is AddItemOffer)
                    {
                        var addItemOffer = offer as AddItemOffer;
                        var offerItemCount = addItemOffer.MinItem + addItemOffer.AddItem;
                        var amountOfOffers = preOfferItemCount / offerItemCount;
                        postOfferItemCount = preOfferItemCount - (amountOfOffers * offerItemCount);

                        var newCount = (amountOfOffers * offerItemCount) - (amountOfOffers * addItemOffer.AddItem);

                        totalPrice += newCount * itemType.Key.Price;
                    }
                    else if (offer is DiscountPriceOffer)
                    {
                        var discountPriceOffer = offer as DiscountPriceOffer;
                        var amountOfOffers = preOfferItemCount / discountPriceOffer.MinItem;

                        postOfferItemCount = preOfferItemCount - (amountOfOffers * discountPriceOffer.MinItem);

                        totalPrice += amountOfOffers * discountPriceOffer.DiscountPrice;
                    }
                    else
                    {
                        // TODO: Should throw an application exception/error here since IOffer isn't handled
                    }

                    totalPrice += postOfferItemCount * itemType.Key.Price;
                }
            }

            return totalPrice;
        }
    }
}