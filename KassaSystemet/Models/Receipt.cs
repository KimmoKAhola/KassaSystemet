using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KassaSystemet.Models
{
    public class Receipt
    {
        public Receipt()
        {

        }
        private const int _receiptPadding = 15;
        private const int _productPadding = 45;
        private const int _amountPadding = 15;
        private const int _pricePadding = 20;
        private const int _sumPadding = 40;
        private static string ReceiptHeader()
        {
            StringBuilder receiptHeader = new StringBuilder();
            int _receiptID = FileManagerOperations.GetReceiptID();
            receiptHeader.AppendLine($"{"Receipt ID:"} {_receiptID}");
            receiptHeader.AppendLine($"{"Product"} {"Amount",_amountPadding} {"Price",_pricePadding}");
            return receiptHeader.ToString();
        }

        private static string ReceiptBody(ShoppingCart shoppingCart)
        {
            StringBuilder receiptBody = new StringBuilder();
            foreach (var item in shoppingCart.Purchases)
            {
                var productName = ProductCatalogue.Instance.Products[item.ProductID].ProductName;
                var amount = item.Amount;
                var price = ProductCatalogue.Instance.Products[item.ProductID].UnitPrice;
                var sum = shoppingCart.CalculateSum(item.ProductID);

                receiptBody.AppendFormat("{0}{1,-15}{2,-20}{3,-40}", productName, amount, price, sum);
                receiptBody.AppendLine();
            }
            return receiptBody.ToString();
        }
        private static string ReceiptBottom(ShoppingCart shoppingCart)
        {
            StringBuilder receiptBottom = new StringBuilder();
            var totalSum = shoppingCart.CalculateTotalSum();
            receiptBottom.AppendFormat("Total sum of purchase: {0:C2}", totalSum);
            return receiptBottom.ToString();
        }


        public static string CreateReceipt(ShoppingCart shoppingCart)
        {
            StringBuilder receipt = new StringBuilder();
            string header = ReceiptHeader();
            string body = ReceiptBody(shoppingCart);
            string bottom = ReceiptBottom(shoppingCart);
            receipt.AppendLine(header);
            receipt.AppendLine(body);
            receipt.AppendLine();
            receipt.AppendLine(bottom);
            return receipt.ToString();
        }
    }
}