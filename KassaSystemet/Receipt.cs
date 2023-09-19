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
        /*Class which creates the receipt after the user uses the PAY command
         */

        public static string GetReceiptFilePath()
        {
            return $"../../../Files/RECEIPT_{GetCurrentDate()}.txt";
        }

        public static string GetReceiptIDFilePath()
        {
            return $"../../../Files/RECEIPT_ID_{GetCurrentDate()}.txt";
        }

        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }

        public static int GetReceiptID()
        {
            if (File.Exists(GetReceiptIDFilePath()) && File.Exists(GetReceiptFilePath())){
                return Convert.ToInt32(File.ReadLines(GetReceiptIDFilePath()).First());
            }
            else
            {
                return 0;
            }
        }
        public static void CreateReceipt(List<Product> productList, int receiptID) // Den här ska ändras till List<Purchase> senare
        {
            using (StreamWriter receiptWriter = new($"{GetReceiptFilePath()}", append: true))
            {
                receiptWriter.Write($"********Receipt ID[{receiptID}]*******************\n");
                foreach (var product in productList)
                {
                    receiptWriter.Write($"Products are: {product.ProductName} with product ID " +
                        $"[{product.ProductID}] and unit price: " +
                        $"[{product.UnitPrice}]\n"); // Loopar igenom alla köp i produktlistan
                }
                receiptWriter.WriteLine("--------------------------------"); // Separation between different purchases
            }
        }

        public static void CreateReceiptForCart(List<Purchase> list, int receiptID)
        {
            using (StreamWriter receiptWriter = new($"{GetReceiptFilePath()}", append: true))
            {
                receiptWriter.Write($"********Receipt ID[{receiptID}]*******************\n");
                foreach (var purchase in list)
                {
                    receiptWriter.Write($"Products are: {purchase.ProductName} with product ID " +
                        $"[{purchase.Amount}]\n"); // Loops through all wares in the shopping cart
                }
                receiptWriter.WriteLine("--------------------------------"); // Separation between different purchases
            }
        }

        public static void CreateReceiptIDFile(int receiptID)
        {
            using (StreamWriter idWriter = new StreamWriter($"{GetReceiptIDFilePath()}", append: false))
            {
                idWriter.Write(receiptID);
            }
        }
    }
}
