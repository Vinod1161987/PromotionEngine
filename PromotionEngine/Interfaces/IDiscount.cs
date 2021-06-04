using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Interfaces
{
    interface IDiscount
    {
        public Guid Id { get; set; }

        public bool IsActive { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        double Apply(List<CartItem> cart);         

    }
}
