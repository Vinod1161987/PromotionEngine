using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Interfaces
{
    public interface ICartManager
    {
        public Cart Cart { get; set; }
        /// <summary>
        /// Add product to cart list
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Guid Add(Product product);

        /// <summary>
        /// Remove product from cart list
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        bool Remove(Guid productId);

        /// <summary>
        /// Checkout products from cart list
        /// </summary>
        /// <returns></returns>
        double Checkout();
    }
}
