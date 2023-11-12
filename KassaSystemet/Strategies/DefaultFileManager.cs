using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Strategy
{
    public class DefaultFileManager : IFileManager
    {
        private static string FormatProductCatalogueToTextFile()
        {
            var productCatalogue = ProductCatalogue.Instance.Products;
            var formattedProductCatalogue = productCatalogue.OrderBy(x => x.Key).Select(item => $"{item.Key}!{item.Value.ProductName}!{item.Value.UnitPrice}!{item.Value.PriceType}\n");
            return string.Join("", formattedProductCatalogue);
        }
        public void SaveDiscountListToFile()
        {
            var discountInfo = FormatDiscountList();

            using (StreamWriter writer = new($"{FileManagerOperations.CreateDiscountListFilePath()}", append: false))
            {
                writer.Write(discountInfo);
            }
        }

        private string FormatDiscountList()
        {
            var allDiscountedProducts = Product.GetDiscountForSingleProduct(ProductCatalogue.Instance.Products).ToList();
            StringBuilder discountInfoBuilder = new StringBuilder();

            foreach (var product in allDiscountedProducts)
            {
                discountInfoBuilder.Append(product.Key);

                for (int i = 0; i < product.Value.Discounts.Count; i++)
                {
                    discountInfoBuilder.Append($"!{product.Value.Discounts[i].StartDate}");
                    discountInfoBuilder.Append($"!{product.Value.Discounts[i].EndDate}");
                    discountInfoBuilder.Append($"!{product.Value.Discounts[i].DiscountPercentage}");
                }

                discountInfoBuilder.AppendLine();
            }
            return discountInfoBuilder.ToString();
        }
        public Dictionary<int, Product> LoadProductListFromFile()
        {
            Dictionary<int, Product> products = new Dictionary<int, Product>();
            if (File.Exists(FileManagerOperations.CreateProductListFilePathText()))
            {
                var productListInfo = File.ReadAllLines(FileManagerOperations.CreateProductListFilePathText());

                foreach (var item in productListInfo)
                {
                    string[] columns = item.Split('!');

                    var product = ModelFactory.CreateProduct(columns[1], Convert.ToDecimal(columns[2]), columns[3]);
                    products.Add(Convert.ToInt32(columns[0]), product);
                }
            }
            else
                products = ProductCatalogue.SeedProducts();

            return products;
        }
        public void LoadDiscountListFromFile()
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
                        var discount = ModelFactory.CreateDiscount(startDate, endDate, discountPercentage);
                        temp[key].AddDiscountToProduct(discount);
                    }
                }
            }
        }
        public void SaveReceiptToFile(string paymentInfo)
        {
            FileManagerOperations.IncrementReceiptCounter();
            using (StreamWriter receiptWriter = new($"{FileManagerOperations.CreateReceiptFilePath()}", append: true))
            {
                receiptWriter.Write(paymentInfo);
            }
        }
        public string LoadInfoMenuFromFile()
        {
            var filePath = FileManagerOperations.CreateInfoMenuFilePath();
            var result = File.ReadAllText(filePath);
            return result;
        }
        public void SaveProductCatalogueToFile()
        {
            string productString = FormatProductCatalogueToTextFile();
            using (StreamWriter productListWriter = new($"{FileManagerOperations.CreateProductListFilePathText()}", append: false))
            {
                productListWriter.Write(productString);
            }
        }
    }
}