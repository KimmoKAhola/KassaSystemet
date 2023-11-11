using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public static class FileManagerOperations
    {
        private static string _filesFolderPath = $"../../../Files";
        private static string _receiptsFolderPath = $"../../../Files/Receipts";
        private static string _productListFolderPath = $"../../../Files/ProductLists";
        public static void CreateFolders()
        {
            CreateDirectoryIfNotExists(_filesFolderPath);
            CreateDirectoryIfNotExists(_receiptsFolderPath);
            CreateDirectoryIfNotExists(_productListFolderPath);
        }
        private static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        public static string CreateReceiptFilePath() => $"{_receiptsFolderPath}/RECEIPT_{DateTime.Now.ToString("yyyyMMdd")}.txt";
        public static string CreateReceiptIDFilePath() => $"{_receiptsFolderPath}/RECEIPT_ID.txt";
        public static string CreateDiscountListFilePath() => $"{_productListFolderPath}/DISCOUNT_LIST_ADMIN.txt";
        public static string CreateProductListFilePathText() => $"{_productListFolderPath}/PRODUCT_LIST_ADMIN.txt";
        public static string CreateProductListFilePathCsv() => $"{_productListFolderPath}/PRODUCT_LIST_ADMIN.csv";
        private static void CreateReceiptIDFile(int receiptID)
        {
            using (StreamWriter idWriter = new StreamWriter($"{CreateReceiptIDFilePath()}", append: false))
            {
                idWriter.Write(receiptID);
            }
        }
        public static void IncrementReceiptCounter()
        {
            int newReceiptID = GetReceiptID() + 1;
            CreateReceiptIDFile(newReceiptID);
        }
        public static int GetReceiptID()
        {
            if (!File.Exists(CreateReceiptIDFilePath()))
                CreateReceiptIDFile(1);

            return Convert.ToInt32(File.ReadLines(CreateReceiptIDFilePath()).First());
        }
    }
}
