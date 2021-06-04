using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public bool IsPriceCalculated { get; set; }
    }
}
