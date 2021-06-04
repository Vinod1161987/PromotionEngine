using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Models
{
    public class Cart
    {
        public Cart()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        public List<CartItem> CartItems { get; } = new List<CartItem>();
        public double TotalPrice { get; set; }
        public DateTime CartDate { get; set; }
    }
}
