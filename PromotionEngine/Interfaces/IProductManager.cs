using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Interfaces
{
    public interface IProductManager
    {
        /// <summary>
        /// Add products to database
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        int Add(Product product);

        /// <summary>
        /// Remove product from database
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        bool Remove(Guid productId);        
    }
}
