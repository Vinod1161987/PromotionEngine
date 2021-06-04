using PromotionEngine.Database;
using PromotionEngine.Interfaces;
using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Business
{
    public class DiscountManager : IDiscountManager
    {
        public DiscountManager()
        {
        }
        public void Apply(List<CartItem> cart)
        {
            var lastDiscountUniqueID = 0;
            foreach(var item in cart)            
            {
                var discount = LocalDB.discounts.Find(x => x.IsActive && x.ProductID.Equals(item.Product.Id));
                if (discount != null)
                {
                    // Discount at SINGLE PRODUCT purchase
                    if (discount.DiscountCategory == DiscountCategory.SingleProduct)
                    {
                       ApplySingleProductDiscount(item, discount);                        
                    }
                    // Discount at MULTI PRODUCT purchase
                    else
                    {                        
                        // Fetch all prouduct ids
                        var multiProductDiscountList = LocalDB.discounts.Where(d => d.DiscountUniqueID == discount.DiscountUniqueID).ToList();

                        List<CartItem> multiProductCartItems = new List<CartItem>();
                        // find out applicable cart items
                        multiProductDiscountList.ForEach(x =>
                        {
                            var items = cart.Where(c => c.Product.Id == x.ProductID);
                            multiProductCartItems.AddRange(items);
                        });

                        // order by ASC so min count item will come first
                        multiProductCartItems = multiProductCartItems.OrderBy(x => x.Count).ToList();
                        
                        
                        var totalPrice = 0.0;
                        // Match discounted product count with cart items
                        if (multiProductDiscountList.Count == multiProductCartItems.Count)
                        {
                            var minProductCountForDiscount = multiProductDiscountList.First();
                            
                            // To check fist cartitme from same discuount product is processing
                            if (lastDiscountUniqueID != minProductCountForDiscount.DiscountUniqueID)
                            {
                                lastDiscountUniqueID = minProductCountForDiscount.DiscountUniqueID;
                                var multiProductCartItemCount = multiProductCartItems.First().Count;
                                // C - 2 - 1 > 20
                                // D - 3 20 + 25
                                // E - 3 20 + 25 + 30
                                
                                if (multiProductCartItemCount >= minProductCountForDiscount.MinProductCount)
                                {
                                    var batches = multiProductCartItemCount / minProductCountForDiscount.MinProductCount; // 2 / 1 = 2
                                    var invidualItems = multiProductCartItemCount % minProductCountForDiscount.MinProductCount; // 2 % 1  = 0
                                    var discountTotalPrice = 0.0;                                    
                                    if (batches > 0)
                                    {
                                        // discount price 20
                                        discountTotalPrice = batches * minProductCountForDiscount.DiscountPrice;
                                        totalPrice = discountTotalPrice;

                                        var totalItems = 0;
                                        multiProductCartItems.ForEach(x =>
                                        {
                                            totalItems++;
                                            if (x.Count >= minProductCountForDiscount.MinProductCount)
                                            {
                                                int remainingExtraProductCount = x.Count - minProductCountForDiscount.MinProductCount;
                                                totalPrice = totalPrice + remainingExtraProductCount * x.Product.Price;

                                            // Reset price to 0 for all items except last one
                                            x.Product.Price = 0.0;

                                            // update price to last product
                                            if (totalItems == multiProductCartItems.Count)
                                                {
                                                    x.Product.Price = totalPrice;
                                                }
                                            }

                                        });
                                    }        
                                    else
                                    {
                                        multiProductCartItems.ForEach(x =>
                                        {
                                            totalPrice += x.Count * x.Product.Price;
                                        });
                                    }
                                }                                
                            }
                        }
                        else
                        {
                            multiProductCartItems.ForEach(x =>
                            {
                                x.Product.Price = x.Count * x.Product.Price;
                            });
                        }
                    }
                }
            }
        }

        private static void ApplySingleProductDiscount(CartItem item, Discount discount)
        {
            var batches = item.Count / discount.MinProductCount;
            var invidualItems = item.Count % discount.MinProductCount;
            if (batches > 0)
            {
                item.Product.Price = (batches * discount.DiscountPrice) + (invidualItems * item.Product.Price);
            }
            else
            {
                item.Product.Price = item.Count * item.Product.Price;
            }
        }
    }
}
