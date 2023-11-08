using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            DateTime timeOfPurchase = DateTime.Now;
            Purchases = new();
        }
        public List<Purchase> Purchases { get; set; }

        public decimal Pay() => 0m;
    }
}
