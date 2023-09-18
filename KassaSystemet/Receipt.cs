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

        static readonly string date = DateTime.Now.ToShortDateString();
        static readonly string filePath = $"../../../Files/RECEIPT_{date}.txt";
        public static Dictionary<int, Product> productDictionary = new();
        public static void CreateReceipt(List<Product> productList, int receiptID)
        {
            using (StreamWriter receiptWriter = new($"{filePath}", append: true))
            {
                receiptWriter.Write($"receipt number is: {receiptID}\n" +
                    $"Products are: {productList[0].ProductName} with product ID [{productList[0].ProductID}] and unit price: [{productList[0].UnitPrice}]\n"); // Måste loopa igenom. Hårdkodar med första värdet nu.
            }
        }
    }
}
