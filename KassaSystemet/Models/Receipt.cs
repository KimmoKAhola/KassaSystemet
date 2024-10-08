﻿using System;
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

        private const int _productPadding = -20;
        private const int _amountPadding = 10;
        private const int _pricePadding = 20;
        private const int _sumPadding = 25;
        private const int _total = -_productPadding + _amountPadding + _pricePadding + _sumPadding;
        private string dashedLine = new string('-', _total);
        private ShoppingCart _shoppingCart;

        private string ReceiptHeader()
        {
            int receiptID = FileManagerOperations.GetReceiptID();

            return
                $@"Receipt ID: {receiptID} - Time of purchase: {_shoppingCart.TimeOfPurchase}
{"Product",_productPadding}{"Amount",_amountPadding}{"Price",_pricePadding}{"Sum",_sumPadding}";
        }
        private StringBuilder ReceiptBody()
        {
            StringBuilder receiptBody = new StringBuilder();
            var totalSum = 0m;
            foreach (var item in _shoppingCart.Purchases)
            {
                var productName = ProductCatalogue.Instance.Products[item.ProductID].ProductName;
                var amount = item.Amount;
                var price = ProductCatalogue.Instance.Products[item.ProductID].UnitPrice;
                var sum = price * amount;
                if (ProductCatalogue.Instance.Products[item.ProductID].HasActiveDiscount())
                    sum *= (1 - ShoppingCart.GetDiscountPercentage(item.ProductID));
                totalSum += sum;

                receiptBody.Append($"{productName,_productPadding}{amount,_amountPadding}{price,_pricePadding:C2}{sum,_sumPadding:C2}");
                if (ProductCatalogue.Instance.Products[item.ProductID].HasActiveDiscount())
                {
                    var percentage = ShoppingCart.GetDiscountPercentage(item.ProductID);

                    receiptBody.Append($"\t---  {percentage:P2} discount.");
                }
                receiptBody.AppendLine();
            }
            receiptBody.AppendLine($"\nTotal sum of purchase: {totalSum:C2}");
            return receiptBody;
        }



        public string CreateReceipt()
        {
            StringBuilder receipt = new StringBuilder();
            var header = ReceiptHeader();
            var body = ReceiptBody().ToString();
            receipt.AppendLine(header);
            receipt.Append(body);
            receipt.Append(dashedLine + "\n");
            //receipt.Append();
            return receipt.ToString();
        }
    }
}