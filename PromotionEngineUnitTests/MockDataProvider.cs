using PromotionEngine.Database;
using PromotionEngine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromotionEngineUnitTests
{
    public static class MockDataProvider
    {
        public static Product GetProduct(string name)
        {
            return LocalDB.products.Find(x => x.Name == name);
        }
    }
}
