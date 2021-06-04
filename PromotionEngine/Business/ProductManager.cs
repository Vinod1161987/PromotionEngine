using PromotionEngine.Database;
using PromotionEngine.Interfaces;
using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Business
{
    public class ProductManager : IProductManager
    {
        public int Add(Product product)
        {
            if (!IsProductExists(product.Name))
            {
                LocalDB.products.Add(product);
                return 1;
            }
            return 0;
        }

        public bool Remove(Guid productId)
        {
            return LocalDB.products.Remove(LocalDB.products.Find(x => x.Id.Equals(productId)));
        }

        private bool IsProductExists(string name)
        {
            return LocalDB.products.Any(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
