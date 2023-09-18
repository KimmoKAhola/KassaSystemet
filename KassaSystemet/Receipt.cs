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
        static string date = DateTime.Now.ToShortDateString();
        static string filePath = $"../../../Files{date}.txt";
        public static Dictionary<int, Product> productDictionary = new Dictionary<int, Product>();
        public static void CreateReceipt(int receiptID)
        {
            using (StreamWriter receiptWriter = new StreamWriter($"../../../Files/RECEIPT_{date}.txt", append: true))
            {
                receiptWriter.Write($"receipt number is: {receiptID}\n");
            }
        }
        //Method for handling file I/O. Saving and reading text files. Creating the receipt?
        //static string date = DateTime.Now.ToShortDateString();

        //public static void SaveToFile(Dictionary<int, Product> dictionary, string filePath)
        //{

        //}

        //StreamWriter writer = new StreamWriter($"../../../Files{date}.txt", append: false);

    }
}
