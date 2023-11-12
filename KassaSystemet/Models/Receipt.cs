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
        public Receipt(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        private const int ProductPadding = -20;
        private const int AmountPadding = 10;
        private const int PricePadding = 20;
        private const int SumPadding = 20;

        private ShoppingCart _shoppingCart;

        private StringBuilder ReceiptHeader()
        {
            StringBuilder receiptHeader = new StringBuilder();
            int receiptID = FileManagerOperations.GetReceiptID();
            receiptHeader.AppendLine($"{"Receipt ID:"} {receiptID} - Time of purchase: {_shoppingCart.TimeOfPurchase}");
            receiptHeader.AppendLine($"{"Product",ProductPadding}{"Amount",AmountPadding}{"Price",PricePadding}{"Sum",SumPadding}");
            return receiptHeader;
        }
        private StringBuilder ReceiptBody()
        {
            StringBuilder receiptBody = new StringBuilder();
            foreach (var item in _shoppingCart.Purchases)
            {
                var productName = ProductCatalogue.Instance.Products[item.ProductID].ProductName;
                var amount = item.Amount;
                var price = ProductCatalogue.Instance.Products[item.ProductID].UnitPrice;
                var sum = _shoppingCart.CalculateSum(item.ProductID);

                receiptBody.AppendLine($"{productName,ProductPadding}{amount,AmountPadding}{price,PricePadding:C2}{sum,SumPadding:C2}");
            }
            return receiptBody;
        }

        private StringBuilder ReceiptBottom()
        {
            StringBuilder receiptBottom = new StringBuilder();
            receiptBottom.AppendLine($"Total sum of purchase: {_shoppingCart.CalculateTotalSum():C2}");
            return receiptBottom;
        }

        public string CreateReceipt()
        {
            StringBuilder receipt = new StringBuilder();
            var header = ReceiptHeader().ToString();
            var body = ReceiptBody().ToString();
            var bottom = ReceiptBottom().ToString();
            receipt.AppendLine(header);
            receipt.AppendLine(body);
            receipt.AppendLine(bottom);
            return receipt.ToString();
        }
    }
}