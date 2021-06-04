using PromotionEngine.Interfaces;
using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PromotionEngine.Database
{
    
    public static class LocalDB
    {
        //private static readonly Lazy<LocalDB> localDB = new Lazy<LocalDB>(() => new LocalDB());

        public static List<Product> products = new List<Product>();
        public static List<Discount> discounts = new List<Discount>();
                
        static LocalDB()
        {
            products.Add(new Product { Id = Guid.NewGuid(), Name = "BatteryCell_A", Price = 50, ProductCategory = ProductCategory.Electronic });
            products.Add(new Product { Id = Guid.NewGuid(), Name = "Honey_B", Price = 30, ProductCategory = ProductCategory.Health });
            products.Add(new Product { Id = Guid.NewGuid(), Name = "Colgate_C", Price = 20, ProductCategory = ProductCategory.Cosmostic });
            products.Add(new Product { Id = Guid.NewGuid(), Name = "Brush_D", Price = 15, ProductCategory = ProductCategory.Cosmostic });
            
            var singleProductsA = products.Where(x => x.Name == "BatteryCell_A").ToList();
            var singleProductsB = products.Where(x => x.Name == "Honey_B").ToList();
            var singleProductCD = products.Where(x => x.Name == "Colgate_C" || x.Name == "Brush_D").ToList();
            singleProductsA.ForEach(x => discounts.Add(new Discount { DiscountCategory = DiscountCategory.SingleProduct, ProductID = x.Id, MinProductCount = 3, DiscountPrice = 130, DiscountUniqueID = 1, IsActive = true }));
            singleProductsB.ForEach(x => discounts.Add(new Discount { DiscountCategory = DiscountCategory.SingleProduct, ProductID = x.Id, MinProductCount = 2, DiscountPrice = 45, DiscountUniqueID = 2, IsActive = true }));
            singleProductCD.ForEach(x => discounts.Add(new Discount { DiscountCategory = DiscountCategory.MultiProduct, ProductID = x.Id, MinProductCount = 1, DiscountPrice = 30, DiscountUniqueID = 3, IsActive = true }));
        }      
    }
}
