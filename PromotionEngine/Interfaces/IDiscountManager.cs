using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngine.Interfaces
{
    interface IDiscountManager
    {
        void Apply(List<CartItem> cart);         

    }
}
