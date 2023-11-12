using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.File_IO
{
    public class DefaultFileManager : IFileManager
    {
        private static string FormatProductCatalogueToTextFile()
        {
            var productCatalogue = ProductCatalogue.Instance.Products;
            var formattedProductCatalogue = productCatalogue.OrderBy(x => x.Key).Select(item => $"{item.Key}!{item.Value.ProductName}!{item.Value.UnitPrice}!{item.Value.PriceType}\n");
            return string.Join("", formattedProductCatalogue);
        }
        public void SaveDiscountCatalogueToFile()
        {
            var discountInfo = FormatDiscountList();

            using (StreamWriter writer = new($"{FileManagerOperations.CreateDiscountListFilePath()}", append: false))
            {
                writer.Write(discountInfo);
            }
        }
        private static string FormatDiscountList()
        {
            var allDiscountedProducts = Product.GetDiscountForSingleProduct(ProductCatalogue.Instance.Products).ToList();
            StringBuilder discountInfoBuilder = new StringBuilder();

            foreach (var product in allDiscountedProducts)
            {
                discountInfoBuilder.Append(product.Key);
                string discount = FormatDiscountForSingleProduct(product.Value);
                discountInfoBuilder.Append(discount);
                discountInfoBuilder.AppendLine();
            }
            return discountInfoBuilder.ToString();
        }
        private static string FormatDiscountForSingleProduct(Product product)
        {
            StringBuilder discountInfoBuilder = new StringBuilder();
            for (int i = 0; i < product.Discounts.Count; i++)
            {
                discountInfoBuilder.Append($"!{product.Discounts[i].StartDate}");
                discountInfoBuilder.Append($"!{product.Discounts[i].EndDate}");
                discountInfoBuilder.Append($"!{product.Discounts[i].DiscountPercentage}");
            }
            return discountInfoBuilder.ToString();
        }
        public Dictionary<int, Product> LoadProductListFromFile()
        {
            Dictionary<int, Product> products = new Dictionary<int, Product>();
            if (File.Exists(FileManagerOperations.CreateProductListFilePathText()))
            {
                var productListInfo = File.ReadAllLines(FileManagerOperations.CreateProductListFilePathText());
                return AddProductsToProductCatalogue(productListInfo);
            }
            else
                products = ProductCatalogue.SeedProducts();

            return products;
        }
        private static Dictionary<int, Product> AddProductsToProductCatalogue(string[] productListInfo)
        {
            Dictionary<int, Product> products = new Dictionary<int, Product>();
            foreach (var item in productListInfo)
            {
                string[] columns = item.Split('!');

                var product = ModelFactory.CreateProduct(columns[1], Convert.ToDecimal(columns[2]), columns[3]);
                products.Add(Convert.ToInt32(columns[0]), product);
            }
            return products;
        }
        public void LoadDiscountListFromFile()
        {
            if (File.Exists(FileManagerOperations.CreateDiscountListFilePath()))
            {
                var discountListInfo = File.ReadAllLines(FileManagerOperations.CreateDiscountListFilePath());
                AddDiscountsToFile(discountListInfo);
            }
        }
        private static void AddDiscountsToFile(string[] discountListInfo)
        {
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
                    ProductCatalogue.Instance.Products[key].AddDiscountToProduct(discount);
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
        public string LoadPersonalMenuFromFile()
        {
            var filePath = FileManagerOperations.CreatePersonalMenuFilePath();
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

        public string LoadInfoMenuFromFile()
        {
            var filePath = FileManagerOperations.CreateInfoMenuFilePath();
            var result = File.ReadAllText(filePath);
            return result;
        }
    }
}