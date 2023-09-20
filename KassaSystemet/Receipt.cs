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

        //! Creates a formatted string. This string is then used in FileManager when it is saved to
        //? a text file
        // TODO Se info om strängformatering här https://learn.microsoft.com/en-us/dotnet/api/system.string.format?view=net-7.0
        // TODO It is not necessary to see the product ID on the receipt. This should be removed later since
        // TODO the product ID is only for internal use.
        public static string CreateReceipt(List<Purchase> list, int receiptID)
        {
            string formattedReceipt = "";
            formattedReceipt += ($"********Receipt ID[{receiptID}]*******************\n");
            foreach (var item in list)
            {
                formattedReceipt += ($"\nProduct: {item.ProductName}" +
                    $"  \tamount: {item.Amount}" +
                    $"  \tprice {Product.FindProductPriceType(Product.productDictionary, item.ProductName)}: {Product.FindProductPrice(Product.productDictionary, item.ProductName)}" +
                    $"  \tsum: {Product.FindProductPrice(Product.productDictionary, item.ProductName) * item.Amount} SEK " +
                    $"  \tproduct id: {Product.GetProductID(Product.productDictionary, item.ProductName)}\n");
            }
            formattedReceipt += "\n--------------------------------";
            return formattedReceipt;
        }
        public static void Test(List<Purchase> purchaseList, Dictionary<string, List<Discount>> allDiscounts)
        {
            foreach (var item in purchaseList)
            {
                string productName = item.ProductName;
                //This finds the price when it is not discounted
                decimal price = Product.FindProductPrice(Product.productDictionary, productName);

                if (allDiscounts.ContainsKey(productName) && Discount.IsProductOnSale(productName))
                {
                    //If discount exists we will remove the percentage. 20 % discountPercentage
                    //gives price * (1-0.2)
                    price *= (1 - Discount.GetCurrentDiscountPercentage(productName));
                }
                Console.WriteLine("Price is:" + price);
            }
        }
    }
}
