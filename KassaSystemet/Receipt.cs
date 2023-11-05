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
        private static readonly int _receiptCounter = FileManager.GetReceiptID();
        private static readonly int _receiptID = _receiptCounter++;
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
                if (products[item.ProductID].Discounts.Count > 0)
                {
                    decimal discountPercentage = products[item.ProductID].Discounts.Max(discount => discount.DiscountPercentage);
                    price = price * (1 - discountPercentage);
                }
                decimal sum = Math.Round(price * item.Amount, 4);
                string productInfo = $"{productName,-45} {item.Amount,-15}{price,-20:C3} {sum,-15:C2}";
                if (products[item.ProductID].Discounts.Count > 0)
                {
                    productInfo += $"{products[item.ProductID].Discounts.Max(discount => discount.DiscountPercentage) * 100m} % discount - original price: {products[item.ProductID].UnitPrice:C2}";
                }
                formattedReceipt += $"{productInfo,-100}\n";
            }
            formattedReceipt += $"{numberOfDashedLines}\n";
            return formattedReceipt;
        }
        public static int GetReceiptID() => _receiptID;
    }
}