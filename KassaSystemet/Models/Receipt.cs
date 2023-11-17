using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using KassaSystemet.File_IO;

namespace KassaSystemet.Models
{
    public class Receipt
    {
        public Receipt(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        private const int ProductPadding = -20;
        private const int AmountPadding = 10;
        private const int PricePadding = 20;
        private const int SumPadding = 20;
        private string dashedLine = new string('-', 70);
        private ShoppingCart _shoppingCart;

        private string ReceiptHeader()
        {
            int receiptID = FileManagerOperations.GetReceiptID();

            return
                $@"Receipt ID: {receiptID} - Time of purchase: {_shoppingCart.TimeOfPurchase}
{"Product",-20}{"Amount",10}{"Price",20}{"Sum",20}";
        }
        private StringBuilder ReceiptBody()
        {
            StringBuilder receiptBody = new StringBuilder();
            foreach (var item in _shoppingCart.Purchases)
            {
                var productName = ProductCatalogue.Instance.Products[item.ProductID].ProductName;
                var amount = item.Amount;
                var price = ProductCatalogue.Instance.Products[item.ProductID].UnitPrice;
                var sum = ShoppingCart.CalculateSum(item.ProductID);

                receiptBody.AppendLine($"{productName,ProductPadding}{amount,AmountPadding}{price,PricePadding:C2}{sum,SumPadding:C2}");
                if (ProductCatalogue.Instance.Products[item.ProductID].HasActiveDiscount())
                {
                    var percentage = ShoppingCart.GetDiscountPercentage(item.ProductID);
                    receiptBody.AppendLine($"\t---  {percentage:P2} discount.");
                }
            }
            return receiptBody;
        }

        private string ReceiptBottom()
        {
            return $@"Total sum of purchase: {_shoppingCart.CalculateTotalSum():C2}";
        }

        public string CreateReceipt()
        {
            StringBuilder receipt = new StringBuilder();
            var header = ReceiptHeader();
            var body = ReceiptBody().ToString();
            var bottom = ReceiptBottom();
            receipt.AppendLine(header);
            receipt.AppendLine(body);
            receipt.AppendLine(bottom);
            receipt.AppendLine(dashedLine);
            return receipt.ToString();
        }
    }
}