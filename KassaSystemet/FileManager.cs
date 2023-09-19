using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public class FileManager
    {
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

        public static void CreateReceiptIDFile(int receiptID)
        {
            using (StreamWriter idWriter = new StreamWriter($"{GetReceiptIDFilePath()}", append: false))
            {
                idWriter.Write(receiptID);
            }
        }

        public static int GetReceiptID()
        {
            if (File.Exists(GetReceiptIDFilePath()) && File.Exists(GetReceiptFilePath()))
            {
                return Convert.ToInt32(File.ReadLines(GetReceiptIDFilePath()).First());
            }
            else
            {
                return 0;
            }
        }
        public static void SaveReceipt(List<Purchase> list, int receiptID)
        {
            string receipt = Receipt.CreateReceipt(list, receiptID);
            using (StreamWriter receiptWriter = new($"{GetReceiptFilePath()}", append: false))
            {
                receiptWriter.Write(receipt);
            }
        }
    }
}
