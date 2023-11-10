using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Strategy
{
    public class FileManagerStrategy : IFileManager
    {
        public void SaveProductCatalogueTextFile(Dictionary<int, Product> productCatalogue)
        {
            string productString = FormatProductCatalogueToTextFile(productCatalogue);
            productString = productString.Substring(0, productString.Length - 1);
            using (StreamWriter productListWriter = new($"{FileManagerOperations.CreateProductListFilePathCsv()}", append: false))
            {
                productListWriter.Write(productString);
            }
        }
        private static string FormatProductCatalogueToTextFile(Dictionary<int, Product> productCatalogue)
        {
            var formattedProductCatalogue = productCatalogue.OrderBy(x => x.Key).Select(item => $"{item.Key}!{item.Value.ProductName}!{item.Value.UnitPrice}!{item.Value.PriceType}");
            return string.Join("", formattedProductCatalogue);
        }

        private static string FormatProductCatalogueToCsvFile()
        {
            var csvLines = ProductCatalogue.Instance.Products.OrderBy(x => x.Key).Select(item =>
            $"{item.Key},{item.Value.ProductName},{item.Value.UnitPrice},{item.Value.PriceType}");

            return string.Join("\n", csvLines);
        }
        public void SaveProductCatalogueCsvFile(Dictionary<int, Product> productCatalogue)
        {
            string csvContent = FormatProductCatalogueToCsvFile();
            using (StreamWriter productCsvWriter = new($"{FileManagerOperations.CreateProductListFilePathCsv()}", append: false))
            {
                productCsvWriter.Write(csvContent);
            }
        }
        public void SaveDiscountList(Dictionary<int, Product> productCatalogue)
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
            using (StreamWriter writer = new($"{FileManagerOperations.CreateDiscountListFilePath()}", append: false))
            {
                writer.Write(discountInfo);
            }
        }

        private string FormatDiscountList()
        {
            var allDiscountedProducts = Product.GetDiscountForSingleProduct(ProductCatalogue.Instance.Products).ToList();


            return string.Join("\n", allDiscountedProducts);
        }
        public Dictionary<int, Product> LoadProductList()
        {
            Dictionary<int, Product> products = new Dictionary<int, Product>();
            if (File.Exists(FileManagerOperations.CreateProductListFilePathCsv()))
            {
                var productListInfo = File.ReadAllLines(FileManagerOperations.CreateProductListFilePathCsv());

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
                products = ProductCatalogue.Instance.SeedProducts();

            return products;
        }
        public void LoadDiscountList()
        {
            var temp = ProductCatalogue.Instance.Products;
            if (File.Exists(FileManagerOperations.CreateDiscountListFilePath()))
            {
                var discountListInfo = File.ReadAllLines(FileManagerOperations.CreateDiscountListFilePath());

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
        public void SaveReceipt(string paymentInfo)
        {
            FileManagerOperations.IncrementReceiptCounter();
            using (StreamWriter receiptWriter = new($"{FileManagerOperations.CreateReceiptFilePath()}", append: true))
            {
                receiptWriter.Write(paymentInfo);
            }
        }
    }
}