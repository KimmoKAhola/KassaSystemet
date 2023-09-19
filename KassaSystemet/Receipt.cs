﻿using System;
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
        
        //Creates a formatted string. This string is then used in FileManager when it is saved to
        //a text file
        public static string CreateReceipt(List<Purchase> list, int receiptID)
        {
            string formattedReceipt = "";
            formattedReceipt += ($"********Receipt ID[{receiptID}]*******************\n");
            foreach (var item in list)
            {
                formattedReceipt += ($"\nProduct: {item.ProductName}" +
                    $"  \tamount: {item.Amount}" +
                    $"  \tprice per unit: {Product.FindProductPrice(Menu.seedDictionary, item.ProductName)}" +
                    $"  \tsum: {Product.FindProductPrice(Menu.seedDictionary, item.ProductName) * item.Amount} SEK " +
                    $"  \tproduct id: {Product.GetProductID(Menu.seedDictionary, item.ProductName)}\n");
            }
            formattedReceipt += "\n--------------------------------";
            return formattedReceipt;
        }  
    }
}
