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
        public static string CreateReceipt(List<Purchase> shoppingCart)
        {
            var products = ProductCatalogue.Instance.Products;
            decimal totalSumOfPurchase = 0;
            var receipt = new StringBuilder();
            int _receiptID = FileManagerOperations.GetReceiptID();
            receipt.AppendLine($"{"Receipt ID:",-15} [{_receiptID}]");
            receipt.AppendLine($"{"Product",-45} {"Amount",-15} {"Price",-20} {"Sum",-40}");

            foreach (var item in shoppingCart)
            {
                Product product = products[item.ProductID];
                string productName = product.ProductName;
                decimal price = product.UnitPrice;

                if (product.HasActiveDiscount())
                {
                    decimal discountPercentage = product.Discounts.Max(discount => discount.DiscountPercentage);
                    price *= (1 - discountPercentage);
                }

                decimal sum = Math.Round(price * item.Amount, 4);
                receipt.AppendFormat("{0,-45} {1,-15}{2,-20:C3} {3,-15:C2}", productName, item.Amount, price, sum);

                if (product.HasActiveDiscount())
                {
                    decimal discountPercentage = product.Discounts.Max(discount => discount.DiscountPercentage);
                    receipt.AppendFormat("{0:P2} discount - original price: {1:C2}", discountPercentage, product.UnitPrice);
                }

                receipt.AppendLine();
                totalSumOfPurchase += sum;
            }
            receipt.AppendLine();
            receipt.AppendLine($"The total sum is: {totalSumOfPurchase:C2}");
            receipt.AppendLine(new string('-', 144));

            return receipt.ToString();
        }
    }
}