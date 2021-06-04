using PromotionEngine.Database;
using PromotionEngine.Interfaces;
using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Business
{
    public class CartManager : ICartManager
    {

        private Cart _cart = null;
        private readonly IDiscountManager _discountManager = null;
        private double _totalPrice = 0;
        public Cart Cart
        {
            get => _cart;
            set => _cart = value;
        }
        public CartManager()
        {
            // we can remove hard code object creation by using DI             
            _discountManager = new DiscountManager();
            _cart = new Cart();
        }

        public Guid Add(Product product)
        {
            var productCartItem = _cart.CartItems.FirstOrDefault(x => x.Product.Id == product.Id);

            // If product does not exists in the cart - Add it
            if (productCartItem == null)
            {
                var cartItem = new CartItem()
                {
                    Id = Guid.NewGuid(),
                    Product = product,
                    Count = 1                    
                };

                _cart.CartItems.Add(cartItem);
                productCartItem = cartItem;
            }
            else
            {
                productCartItem.Count++;
            }
            _cart.TotalPrice = CalculateTotalCartPrice(_cart.CartItems);
            return product.Id;
        }

        private double CalculateTotalCartPrice(List<CartItem> cart)
        {
            var _totalPrice = cart.Where(x => x.IsPriceCalculated == false).Sum(x => x.Product.Price * x.Count);
            return _totalPrice;
        }
        public bool Remove(Guid productId)
        {
            var items = _cart.CartItems.RemoveAll(x => x.Id == productId);

            return items > 0;
        }

        public double Checkout()
        {
            //"BatteryCell_A" //"Honey_B"  //"Colgate_C"  //"Brush_D"  // Get discounts
            _discountManager.Apply(_cart.CartItems);

            //Apply discounts
            _cart.TotalPrice = _cart.CartItems.Where(x => x.IsPriceCalculated == false).Sum(x => x.Product.Price);
            return _cart.TotalPrice;
        }

    }
}
