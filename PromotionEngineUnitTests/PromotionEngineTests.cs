using PromotionEngine.Business;
using PromotionEngine.Database;
using PromotionEngine.Interfaces;
using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace PromotionEngineUnitTests
{
    public class PromotionEngineTests
    {
        private IProductManager productManager = new ProductManager();
        private ICartManager cartManager = new CartManager();
        [Fact]
        public void AddRemoveProduct_Valid()
        {
            var product = new Product { Name = "BigToothBrush", Price = 50, ProductCategory = ProductCategory.Cosmostic };

            var count = productManager.Add(product);
            Assert.True(count == 1);
            
            // Remove added product
            var isDeleted = productManager.Remove(product.Id);
            Assert.True(isDeleted);            
        }

        [Fact]
        public void AddProduct_Existing()
        {
            var product = new Product { Name = "BatteryCell_A", Price = 50, ProductCategory = ProductCategory.Cosmostic };
            var count = productManager.Add(product);
            Assert.True(count == 0);

            // Remove added product
            var isDeleted = productManager.Remove(product.Id);
            Assert.False(isDeleted);
        }

        [Fact]
        public void AddItemToCart_WithNoDiscountApplicable()
        {
            AddProductToCart("BatteryCell_A", 1);
            Assert.True(cartManager.Cart.CartItems.Count == 1);
        }
        [Fact]
        public void AddSingleItemToCart_WithADiscountApplicable()
        {
            AddProductToCart("BatteryCell_A", 3);
            Assert.True(cartManager.Cart.CartItems.First().Count == 3);

            cartManager.Checkout();
            Assert.True(cartManager.Cart.TotalPrice == 130);
            Assert.True(cartManager.Cart.CartItems.First().Product.Price == 130);
        }

        [Fact]
        public void AddSingleItemToCart_WithABDiscountApplicable()
        {
            AddProductToCart("BatteryCell_A", 3);
            AddProductToCart("Honey_B", 2);
            Assert.True(cartManager.Cart.CartItems.Count == 2);
            Assert.True(cartManager.Cart.CartItems.First().Count == 3);
            Assert.True(cartManager.Cart.CartItems.Last().Count == 2);

            cartManager.Checkout();
            Assert.True(cartManager.Cart.TotalPrice == 175);
            Assert.True(cartManager.Cart.CartItems.First().Product.Price == 130);
            Assert.True(cartManager.Cart.CartItems.Last().Product.Price == 45);
        }
        [Fact]
        public void AddSingleItemToCart_WithABDiscountApplicableWithNonApplicableProduct()
        {            
            AddProductToCart("BatteryCell_A", 4);
            AddProductToCart("Honey_B", 2);
            Assert.True(cartManager.Cart.CartItems.Count == 2);
            Assert.True(cartManager.Cart.CartItems.First().Count == 4);
            Assert.True(cartManager.Cart.CartItems.Last().Count == 2);

            cartManager.Checkout();
            Assert.True(cartManager.Cart.TotalPrice == 225);
            Assert.True(cartManager.Cart.CartItems.First().Product.Price == 180);
            Assert.True(cartManager.Cart.CartItems.Last().Product.Price == 45);
        }

        [Fact]
        public void AddMultiProductItemToCart_WithoutDiscountApplicableProduct()
        {
            AddProductToCart("Colgate_C", 2);
            //AddProductToCart("Brush_D", 2);
            Assert.True(cartManager.Cart.CartItems.Count == 1);

            cartManager.Checkout();
            Assert.True(cartManager.Cart.TotalPrice == 60);
            Assert.True(cartManager.Cart.CartItems.First().Product.Price == 60);
        }

        [Fact]
        public void AddMultiProductItemToCart_WithDiscountApplicableForSameNoOfProduct()
        {
            AddProductToCart("Colgate_C", 1);
            AddProductToCart("Brush_D", 1);
            Assert.True(cartManager.Cart.CartItems.Count == 2);

            cartManager.Checkout();
            Assert.True(cartManager.Cart.TotalPrice == 30);
            Assert.True(cartManager.Cart.CartItems.First().Product.Price == 0);
            Assert.True(cartManager.Cart.CartItems.Last().Product.Price == 30);
        }

        [Fact]
        public void AddMultiProductItemToCart_WithDiscountApplicableForSameNoOfProduct1()
        {
            AddProductToCart("Colgate_C", 2);
            AddProductToCart("Brush_D", 1);
            Assert.True(cartManager.Cart.CartItems.Count == 2);

            cartManager.Checkout();
            Assert.True(cartManager.Cart.TotalPrice == 50);
        }

        [Fact]
        public void AddMultiProductItemToCart_WithDiscountApplicableForSameNoOfProduct2()
        {
            AddProductToCart("Colgate_C", 1);
            AddProductToCart("Brush_D", 2);
            Assert.True(cartManager.Cart.CartItems.Count == 2);

            cartManager.Checkout();
            Assert.True(cartManager.Cart.TotalPrice == 45);
        }

        private void AddProductToCart(string name, int count)
        {
            for (int i = 0; i < count; i++)
            {
                cartManager.Add(MockDataProvider.GetProduct(name));
            }
        }
    }
}
