using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public class FileManager
    {
        /* This class creates the text file which is saved to the hard drive
         * The methods here are used to create the filepath
         * and are also responsible to keep track of the receipt id
         */
        /// <summary>
        /// Creates a folder for all the files.
        /// </summary>
        private static void CreateFolder()
        {
            Directory.CreateDirectory(GetDirectoryFilePath());
        }
        /// <summary>
        /// Returns a directory file path as a string.
        /// </summary>
        /// <returns></returns>
        private static string GetDirectoryFilePath()
        {
            return $"../../../Files";
        }
        /// <summary>
        /// Returns a file path string for the receipt.
        /// </summary>
        /// <returns></returns>
        private static string CreateReceiptFilePath()
        {
            return $"../../../Files/RECEIPT_{GetCurrentDate()}.txt";
        }
        /// <summary>
        /// Returns a date string for the receipt ID file.
        /// </summary>
        /// <returns></returns>
        private static string CreateReceiptIDFilePath()
        {
            return $"../../../Files/RECEIPT_ID_{GetCurrentDate()}.txt";
        }
        /// <summary>
        /// Returns the file path for the discount file as a string.
        /// </summary>
        /// <returns></returns>
        private static string CreateDiscountListFilePath()
        {
            return "../../../Files/DISCOUNT_LIST_ADMIN.txt";
        }
        /// <summary>
        /// Returns the product list file path as a string.
        /// </summary>
        /// <returns></returns>
        private static string CreateProductListFilePath()
        {
            return "../../../Files/PRODUCT_LIST_ADMIN.txt";
        }
        /// <summary>
        /// Returns the current date as a string on the format "yyyyMMdd".
        /// </summary>
        /// <returns></returns>
        private static string GetCurrentDate()
        {
            return DateTime.Now.ToString("yyyyMMdd");
        }
        /// <summary>
        /// Creates the file for the receipt ID file. This is where the receipt ID is saved after each purchase.
        /// </summary>
        /// <param name="receiptID"></param>
        private static void CreateReceiptIDFile(int receiptID)
        {
            using (StreamWriter idWriter = new StreamWriter($"{CreateReceiptIDFilePath()}", append: false))
            {
                idWriter.Write(receiptID);
            }
        }
        /// <summary>
        /// Increments the receipt ID after each purchase.
        /// </summary>
        /// <returns></returns>
        public static int IncrementReceiptCounter()
        {
            int currentReceiptID = GetReceiptID();
            int newReceiptID = currentReceiptID + 1;
            CreateReceiptIDFile(newReceiptID); // Update the receipt ID file
            return newReceiptID;
        }
        /// <summary>
        /// Returns the receipt ID. Loads the ID from the receipt ID file.
        /// </summary>
        /// <returns></returns>
        public static int GetReceiptID()
        {
            if (!File.Exists(CreateReceiptIDFilePath()))
            {
                CreateReceiptIDFile(Receipt.receiptID);
            }
            return Convert.ToInt32(File.ReadLines(CreateReceiptIDFilePath()).First());
        }
        /// <summary>
        /// Saves the receipt to a txt file. Receives the string from the receipt class.
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <param name="receiptID"></param>
        public static void SaveReceipt(List<Purchase> shoppingCart, int receiptID)
        {
            if (!Directory.Exists(GetDirectoryFilePath()))
            {
                CreateFolder();
            }
            IncrementReceiptCounter();
            string receipt = Receipt.CreateReceipt(shoppingCart, receiptID);
            using (StreamWriter receiptWriter = new($"{CreateReceiptFilePath()}", append: true))
            {
                receiptWriter.Write(receipt);
            }
        }
        /// <summary>
        /// Saves the product list to a txt file. Receives the product string from the product class.
        /// </summary>
        /// <param name="productDictionary"></param>
        public static void SaveProductList(Dictionary<int, Product> productDictionary)
        {
            string productListString = Product.CreateProductString(productDictionary);
            using (StreamWriter writer = new($"{CreateProductListFilePath()}", append: false))
            {
                writer.Write(productListString);
            }
        }
        /// <summary>
        /// Saves the discount list to a txt file. Receives the discount list from the Discount class.
        /// </summary>
        /// <param name="allDiscounts"></param>
        public static void SaveDiscountList(Dictionary<int, List<Discount>> allDiscounts)
        {
            string discountListString = Discount.CreateDiscountString(allDiscounts);
            using (StreamWriter writer = new($"{CreateDiscountListFilePath()}", append: false))
            {
                writer.Write(discountListString);
            }
        }
        /// <summary>
        /// Loads the product list from a txt file each time the program is loaded.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, Product> LoadProductList()
        {
            if (File.Exists(CreateProductListFilePath()))
            {
                var productListInfo = File.ReadAllLines(CreateProductListFilePath());
                Product.productDictionary.Clear();
                foreach (var item in productListInfo)
                {
                    string[] columns = item.Split('!');
                    for (int i = 0; i < columns.Length; i += 4)
                    {
                        Product.productDictionary.Add(Convert.ToInt32(columns[i]), new Product(columns[i + 1], Convert.ToDecimal(columns[i + 2]), columns[i + 3]));
                    }
                }
            }
            return Product.productDictionary;
        }
        /// <summary>
        /// Loads the discount list when the program starts.
        /// </summary>
        /// <param name="discountList"></param>
        /// <returns></returns>
        public static Dictionary<int, List<Discount>> LoadDiscountList(Dictionary<int, List<Discount>> discountList)
        {
            if (File.Exists(CreateDiscountListFilePath()))
            {
                var discountListInfo = File.ReadLines(CreateDiscountListFilePath());
                discountList.Clear();
                foreach (var item in discountListInfo)
                {
                    List<Discount> temp = new();
                    string[] columns = item.Split('!');
                    int productID = Convert.ToInt32(columns[0]);
                    for (int i = 1; i < columns.Length - 1; i += 3)
                    {
                        string startDate = columns[i];
                        string endDate = columns[i + 1];
                        decimal discountPercentage = Convert.ToDecimal(columns[i + 2]);
                        temp.Add(new Discount(startDate, endDate, discountPercentage));
                    }
                    discountList.Add(productID, temp);
                }
            }
            return discountList;
        }
    }
}
