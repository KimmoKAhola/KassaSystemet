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
        public static string CreateReceipt(List<Purchase> shoppingCart, int receiptID)
        {
            //TODO need to print out the discounted price if there is a discount
            string formattedReceipt = "";
            formattedReceipt += ($"\n********Receipt ID[{receiptID}]*******************\n");
            foreach (var item in shoppingCart)
            {
                string productName = Product.GetProductName(Product.productDictionary, item.ProductID);
                decimal price = Product.FindProductPrice(Product.productDictionary, item.ProductID);
                decimal discount = Discount.GetCurrentDiscountPercentage(productName) * 100m;
                formattedReceipt += ($"\nProduct: {productName}" +
                    $"  \tamount: {item.Amount}");

                if (Discount.allDiscounts.ContainsKey(productName) && Discount.IsProductOnSale(productName))
                {
                    price *= (1 - Discount.GetCurrentDiscountPercentage(productName));
                    formattedReceipt += ($"  \tDiscounted price ({discount} % off) {Product.FindProductPriceType(Product.productDictionary, item.ProductID)}: {price}" +
                    $"  \tsum: {price * item.Amount} SEK");
                }
                else
                {
                    price = Product.FindProductPrice(Product.productDictionary, item.ProductID);
                    formattedReceipt += ($"\tprice {Product.FindProductPriceType(Product.productDictionary, item.ProductID)}: {price}" +
                    $"  \tsum: {price * item.Amount} SEK");
                }
            }
            formattedReceipt += "\n--------------------------------";
            return formattedReceipt;
        }
    }
}
