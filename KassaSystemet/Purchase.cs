using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    internal class Purchase
    {

        //Class which handles the purchase list

        public Purchase(string productID, int amount) 
        {
            productID = ProductID;
            amount = Amount;
        }
        
        public string ProductID { get; set; }
        public int Amount { get; set; }
    }
}
