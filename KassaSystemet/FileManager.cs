﻿using System;
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

        public static int IncrementReceiptCounter()
        {
            int currentReceiptID = GetReceiptID();
            int newReceiptID = currentReceiptID + 1;
            CreateReceiptIDFile(newReceiptID); // Update the receipt ID file
            return newReceiptID;
        }
        public static int GetReceiptID()
        {
            if (!File.Exists(GetReceiptIDFilePath()))
            {
                CreateReceiptIDFile(Receipt.receiptID);
            }
            return Convert.ToInt32(File.ReadLines(GetReceiptIDFilePath()).First());
        }
        public static void SaveReceipt(List<Purchase> list, int receiptID)
        {
            
            string receipt = Receipt.CreateReceipt(list, receiptID);
            receiptID++;
            using (StreamWriter receiptWriter = new($"{GetReceiptFilePath()}", append: true))
            {
                receiptWriter.Write(receipt);
            }
        }
    }
}
