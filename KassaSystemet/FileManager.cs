using System;
using System.Collections.Generic;
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

        private static string CreateReceiptFilePath()
        {
            return $"../../../Files/RECEIPT_{GetCurrentDate()}.txt";
        }
        private static string CreateReceiptIDFilePath()
        {
            return $"../../../Files/RECEIPT_ID_{GetCurrentDate()}.txt";
        }
        private static string CreateDiscountListFilePath()
        {
            return "../../../Files/DISCOUNT_LIST_ADMIN.txt";
        }
        private static string CreateProductListFilePath()
        {
            return "../../../Files/PRODUCT_LIST_ADMIN.txt";
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
                CreateReceiptIDFile(Receipt.receiptID);
            }
            return Convert.ToInt32(File.ReadLines(CreateReceiptIDFilePath()).First());
        }
        public static void SaveReceipt(List<Purchase> shoppingCart, int receiptID)
        {
            IncrementReceiptCounter();
            string receipt = Receipt.CreateReceipt(shoppingCart, receiptID);
            using (StreamWriter receiptWriter = new($"{CreateReceiptFilePath()}", append: true))
            {
                receiptWriter.Write(receipt);
            }
        }

        public static void SaveProductList(Dictionary<int, Product> productDictionary)
        {
            string productListString = Product.CreateProductString(productDictionary);
            using (StreamWriter writer = new($"{CreateProductListFilePath()}", append: false))
            {
                writer.Write(productListString);
            }
        }

        public static void SaveDiscountList(Dictionary<string, List<Discount>> allDiscounts)
        {
            string discountListString = Discount.CreateDiscountString(allDiscounts);
            using(StreamWriter writer = new($"{CreateDiscountListFilePath()}", append: false))
            {
                writer.Write(discountListString);
            }
        }

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
        public static Dictionary<string, List<Discount>> LoadDiscountList()
        {
            if (File.Exists(CreateDiscountListFilePath()))
            {
                var discountListInfo = File.ReadLines(CreateDiscountListFilePath());
                //Discount.allDiscounts.Clear();
                foreach (var item in discountListInfo)
                {
                    string[] columns = item.Split(!);

                }
            }
        }
    }
}
