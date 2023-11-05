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
        private static readonly int _receiptCounter = FileManager.GetReceiptID(); // Load receipt ID from file
        private static readonly int _receiptID = _receiptCounter++; // These work as long as the receipt files are not deleted

        public static string CreateReceipt(List<Purchase> shoppingCart, Dictionary<int, Product> products)
        {
            string formattedReceipt = "";
            formattedReceipt += ($"{"Receipt ID: ",-5} [{_receiptID}]\n");
            formattedReceipt += ($"{"Product",-45} {"Amount",-15} {"Price",-20} {"Sum",-40}\n");
            string numberOfDashedLines = new string('-', formattedReceipt.Length);
            foreach (var item in shoppingCart)
            {
                string productName = products[item.ProductID].ProductName;
                int productID = item.ProductID;
                decimal price = products[item.ProductID].UnitPrice;
                //if product contains discount
                //decimal discount = Discount.GetCurrentDiscountPercentage(productID) * 100m;
                //string priceType = products[item.ProductID].PriceType;

                decimal sum = Math.Round(price * item.Amount, 4);

                string productInfo = $"{productName,-45} {item.Amount,-15}{price,-20:C3} {sum,-15:C2}";

                formattedReceipt += $"{productInfo,-100}\n";
            }
            formattedReceipt += $"{numberOfDashedLines}\n";
            return formattedReceipt;
        }

        public static int GetReceiptID()
        {
            return _receiptID;
        }
    }
}
