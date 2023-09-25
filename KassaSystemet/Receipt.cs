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
        public static int receiptCounter = FileManager.GetReceiptID(); // Load receipt ID from file
        public static int receiptID = receiptCounter++; // These work as long as the receipt files are not deleted

        public static string CreateReceipt(List<Purchase> shoppingCart, int receiptID)
        {
            string formattedReceipt = "";
            formattedReceipt += ($"{"Receipt ID: ",-5} [{receiptID}]\n");
            formattedReceipt += ($"{"Product",-45} {"Amount",-15} {"Price",-20} {"Sum",-40}\n");
            string numberOfDashedLines = new string('-', formattedReceipt.Length);
            foreach (var item in shoppingCart)
            {
                string productName = Product.GetProductName(Product.productDictionary, item.ProductID);
                int productID = Product.GetProductID(Product.productDictionary, productName);
                decimal price = Product.FindProductPrice(Product.productDictionary, item.ProductID);
                if (Discount.allDiscounts.ContainsKey(productID) && Discount.IsProductOnSale(productID))
                {
                    price *= (1 - Discount.GetCurrentDiscountPercentage(productID));
                }
                decimal discount = Discount.GetCurrentDiscountPercentage(productID) * 100m;
                string priceType = Product.FindProductPriceType(Product.productDictionary, item.ProductID);

                decimal sum = Math.Round(price * item.Amount, 4);

                string productInfo = ($"{productName,-45} {item.Amount,-15}{price,-20:C3} {sum,-15:C2}");

                if (Discount.allDiscounts.ContainsKey(productID) && Discount.IsProductOnSale(productID))
                {
                    string discountInfo = $"---- {discount} % discount!";
                    formattedReceipt += $"{productInfo,-10} {discountInfo,-10}\n";
                }
                else
                {
                    formattedReceipt += $"{productInfo,-100}\n";
                }
            }
            formattedReceipt += $"{numberOfDashedLines}\n";
            return formattedReceipt;
        }
    }
}
