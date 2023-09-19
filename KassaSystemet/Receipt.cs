using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KassaSystemet
{
    public static class Receipt
    {
        /* A class which creates a formatted receipt
         */

        public static string CreateReceipt(List<Purchase> list, int receiptID)
        {
            string formattedReceipt = "";
            formattedReceipt += ($"********Receipt ID[{receiptID}]*******************\n");
            foreach (var item in list)
            {
                formattedReceipt += ($"\nProduct: {item.ProductName}" +
                    $"  \tamount: {item.Amount}" +
                    $"  \tprice per unit: {Product.FindProductPrice(Menu.seedDictionary, item.ProductName)}" +
                    $"  \tsum: {Product.FindProductPrice(Menu.seedDictionary, item.ProductName) * item.Amount} SEK " +
                    $"  \tproduct id: {Product.GetProductID(Menu.seedDictionary, item.ProductName)}\n");
            }
            formattedReceipt += "\n--------------------------------";
            return formattedReceipt;
        }  
    }
}
