using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Models
{
     public class Discount
    {
        public DiscountCategory DiscountCategory { get; set; }
        public int DiscountUniqueID { get; set; }
        public Guid ProductID { get; set; }
        public int MinProductCount { get; set; }
        public double DiscountPrice { get; set; }
        public DateTime StartTime { get; set; }
        public string Description { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
    }
}
