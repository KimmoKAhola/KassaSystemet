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
            string filePath = GetDirectoryFilePath();
            string receiptsPath = GetDirectoryReceiptsFilePath();
            string productsPath = GetDirectoryProductsFilePath();

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(GetDirectoryFilePath());
            if (!Directory.Exists(receiptsPath))
                Directory.CreateDirectory(GetDirectoryReceiptsFilePath());
            if (!Directory.Exists(productsPath))
                Directory.CreateDirectory(GetDirectoryProductsFilePath());
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
            string productString = "";
            foreach (var item in products)
            {
                productString += item.Key;
                productString += "!" + item.Value.ProductName;
                productString += "!" + item.Value.UnitPrice;
                productString += "!" + item.Value.PriceType + "!";
            }
            productString = productString.Substring(0, productString.Length - 1);
            using (StreamWriter productListWriter = new($"{CreateProductListFilePath()}", append: false))
            {
                productListWriter.Write(productString);
            }
        }

        public static void SaveDiscountList(Dictionary<int, Product> products)
        {
            var allDiscountedProducts = Product.GetDiscountInfo(products).ToList();
            string discountInfo = "";
            foreach (var product in allDiscountedProducts)
            {
                discountInfo += product.Key;
                for (int i = 0; i < product.Value.Discounts.Count; i++)
                {
                    discountInfo += "!" + product.Value.Discounts[i].StartDate;
                    discountInfo += "!" + product.Value.Discounts[i].EndDate;
                    discountInfo += "!" + product.Value.Discounts[i].DiscountPercentage;
                }
                discountInfo += "\n";
            }
            using (StreamWriter writer = new($"{CreateDiscountListFilePath()}", append: false))
            {
                writer.Write(discountInfo);
            }
        }

        public static Dictionary<int, Product> LoadProductList()
        {
            Dictionary<int, Product> products = new Dictionary<int, Product>();
            if (File.Exists(CreateProductListFilePath()))
            {
                var productListInfo = File.ReadAllLines(CreateProductListFilePath());

                foreach (var item in productListInfo)
                {
                    string[] columns = item.Split('!');

                    for (int i = 0; i < columns.Length; i += 4)
                    {
                        Product p = new Product(columns[i + 1], Convert.ToDecimal(columns[i + 2]), columns[i + 3]);
                        products.Add(Convert.ToInt32(columns[i]), p);
                    }
                }
            }
            else
            {
                products = ProductDataBase.SeedProducts();
            }
            return products;
        }

        public static void LoadDiscountList(Dictionary<int, Product> products)
        {
            if (File.Exists(CreateDiscountListFilePath()))
            {
                var discountListInfo = File.ReadAllLines(CreateDiscountListFilePath());

                foreach (var item in discountListInfo)
                {
                    string[] columns = item.Split('!');
                    int key = Convert.ToInt32(columns[0]);
                    for (int i = 1; i < columns.Length; i += 3)
                    {
                        string startDate = columns[i];
                        string endDate = columns[i + 1];
                        decimal discountPercentage = Convert.ToDecimal(columns[i + 2]) * 100m;
                        Discount d = new Discount(startDate, endDate, discountPercentage);
                        products[key].AddDiscount(d);
                    }
                }
            }
        }
    }
}
