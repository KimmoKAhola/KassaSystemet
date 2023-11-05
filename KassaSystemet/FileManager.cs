using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public static class FileManager
    {
        /* This class creates the text file which is saved to the hard drive
         * The methods here are used to create the filepath
         * and are also responsible to keep track of the receipt id
         */
        /// <summary>
        /// Creates a folder for all the files.
        /// </summary>
        public static void CreateFolders()
        {
            if (!Directory.CreateDirectory(GetDirectoryFilePath()).Exists)
            {
                Directory.CreateDirectory(GetDirectoryFilePath());
            }
            if (!Directory.CreateDirectory(GetDirectoryReceiptsFilePath()).Exists)
            {
                Directory.CreateDirectory(GetDirectoryReceiptsFilePath());
            }
            if (!Directory.CreateDirectory(GetDirectoryProductsFilePath()).Exists)
            {
                Directory.CreateDirectory(GetDirectoryProductsFilePath());
            }
        }
        private static string GetDirectoryFilePath()
        {
            return $"../../../Files";
        }
        private static string GetDirectoryReceiptsFilePath()
        {
            return $"../../../Files/Receipts";
        }
        private static string GetDirectoryProductsFilePath()
        {
            return $"../../../Files/ProductLists";
        }
        private static string CreateReceiptFilePath()
        {
            string filePath = GetDirectoryReceiptsFilePath();
            return $"{filePath}/RECEIPT_{GetCurrentDate()}.txt";
        }
        private static string CreateReceiptIDFilePath()
        {
            string filePath = GetDirectoryReceiptsFilePath();
            return $"{filePath}/RECEIPT_ID.txt";
        }
        private static string CreateDiscountListFilePath()
        {
            string filePath = GetDirectoryProductsFilePath();
            return $"{filePath}/DISCOUNT_LIST_ADMIN.txt";
        }
        private static string CreateProductListFilePath()
        {
            string filePath = GetDirectoryProductsFilePath();
            return $"{filePath}/PRODUCT_LIST_ADMIN.txt";
        }
        private static string GetCurrentDate()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }
        private static void CreateReceiptIDFile(int receiptID)
        {
            using (StreamWriter idWriter = new StreamWriter($"{CreateReceiptIDFilePath()}", append: false))
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
            if (!File.Exists(CreateReceiptIDFilePath()))
            {
                int id = Receipt.GetReceiptID();
                CreateReceiptIDFile(id);
                return 1;
            }
            return Convert.ToInt32(File.ReadLines(CreateReceiptIDFilePath()).First());
        }

        public static void SaveReceipt(string paymentInfo)
        {
            if (!Directory.Exists(GetDirectoryFilePath()))
            {
                CreateFolders();
            }
            IncrementReceiptCounter();
            using (StreamWriter receiptWriter = new($"{CreateReceiptFilePath()}", append: true))
            {
                receiptWriter.Write(paymentInfo);
            }
        }

        public static void SaveProductList(Dictionary<int, Product> products)
        {

        }

        public static void SaveDiscountList()
        {
            //string discountListString = Discount.CreateDiscountString(allDiscounts);

            using (StreamWriter writer = new($"{CreateDiscountListFilePath()}", append: false))
            {
                //writer.Write(discountListString);
            }
        }

        public static void LoadProductList()
        {
            if (File.Exists(CreateProductListFilePath()))
            {
                var productListInfo = File.ReadAllLines(CreateProductListFilePath());

                foreach (var item in productListInfo)
                {
                    string[] columns = item.Split('!');
                    for (int i = 0; i < columns.Length; i += 4)
                    {

                    }
                }
            }

        }

        public static Dictionary<int, List<Discount>> LoadDiscountList()
        {
            return new Dictionary<int, List<Discount>>();
        }
    }
}
