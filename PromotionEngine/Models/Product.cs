using PromotionEngine.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PromotionEngine.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public string Description { get; set; }        
    }
}
