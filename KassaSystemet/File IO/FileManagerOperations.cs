using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.File_IO
{
    public static class FileManagerOperations
    {
        private static string _filesFolderPath = $"../../../Files";
        private static string _receiptsFolderPath = $"../../../Files/Receipts";
        private static string _productListFolderPath = $"../../../Files/ProductLists";
        private static string _discountListFolderPath = $"../../../Files/DiscountLists";
        private static string _seededDataFolderPath = $"../../../Data Seeding";
        public static void CreateFolders()
        {
            CreateDirectoryIfNotExists(_filesFolderPath);
            CreateDirectoryIfNotExists(_receiptsFolderPath);
            CreateDirectoryIfNotExists(_productListFolderPath);
            CreateDirectoryIfNotExists(_discountListFolderPath);
        }
        private static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        public static string CreateReceiptFilePath() => Path.Combine(_receiptsFolderPath, $"RECEIPT_{DateTime.Now.ToString("yyyyMMdd")}.txt");
        public static string CreateReceiptIDFilePath() => Path.Combine(_receiptsFolderPath, $"RECEIPT_ID.txt");
        public static string CreatePersonalMenuFilePath() => Path.Combine(_seededDataFolderPath, $"PersonalMenu.txt");
        public static string CreateInfoMenuFilePath() => Path.Combine(_seededDataFolderPath, $"InfoMenu.txt");
        public static string CreateSeededProductsFilePath() => Path.Combine(_seededDataFolderPath, $"SeededProducts.txt");
        public static string CreateDiscountListFilePath() => Path.Combine(_discountListFolderPath, $"DISCOUNT_LIST_ADMIN.txt");
        public static string CreateProductListFilePathText() => Path.Combine(_productListFolderPath, $"PRODUCT_LIST_ADMIN.txt");
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
