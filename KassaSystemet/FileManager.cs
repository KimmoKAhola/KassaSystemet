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
        private static readonly string _filesFolderPath = $"../../../Files";
        private static readonly string _receiptsFolderPath = $"../../../Files/Receipts";
        private static readonly string _productListFolderPath = $"../../../Files/ProductLists";
        private static Dictionary<int, Product> productCatalogue = ProductCatalogue.Instance.Products;
        public static void CreateFolders()
        {
            CreateDirectoryIfNotExists(_filesFolderPath);
            CreateDirectoryIfNotExists(_receiptsFolderPath);
            CreateDirectoryIfNotExists(_productListFolderPath);
        }
        private static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
        private static string CreateReceiptFilePath() => $"{_receiptsFolderPath}/RECEIPT_{DateTime.Now.ToString("yyyyMMdd")}.txt";
        private static string CreateReceiptIDFilePath() => $"{_receiptsFolderPath}/RECEIPT_ID.txt";
        private static string CreateDiscountListFilePath() => $"{_productListFolderPath}/DISCOUNT_LIST_ADMIN.txt";
        private static string CreateProductListFilePath() => $"{_productListFolderPath}/PRODUCT_LIST_ADMIN.txt";
        private static void CreateReceiptIDFile(int receiptID)
        {
            using (StreamWriter idWriter = new StreamWriter($"{CreateReceiptIDFilePath()}", append: false))
            {
                idWriter.Write(receiptID);
            }
        }
        private static void IncrementReceiptCounter()
        {
            int newReceiptID = GetReceiptID() + 1;
            CreateReceiptIDFile(newReceiptID);
        }
        public static int GetReceiptID()
        {
            if (!File.Exists(CreateReceiptIDFilePath()))
            {
                int id = 1;
                CreateReceiptIDFile(id);
                return id;
            }
            return Convert.ToInt32(File.ReadLines(CreateReceiptIDFilePath()).First());
        }
        public static void SaveReceipt(string paymentInfo)
        {
            IncrementReceiptCounter();
            using (StreamWriter receiptWriter = new($"{CreateReceiptFilePath()}", append: true))
            {
                receiptWriter.Write(paymentInfo);
            }
        }
        public static void SaveProductList()
        {
            var temp = productCatalogue.OrderBy(x => x.Key);
            string productString = "";
            foreach (var item in temp)
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
        public static void SaveDiscountList()
        {
            var allDiscountedProducts = Product.GetDiscountForSingleProduct(productCatalogue).ToList();
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
                products = ProductCatalogue.SeedProducts();

            return products;
        }
        public static void LoadDiscountList()
        {
            var temp = productCatalogue;
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
                        temp[key].AddDiscountToProduct(d);
                    }
                }
            }
        }
    }
}