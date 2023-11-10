using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Strategy
{
    public class FileManagerStrategy : IFileManagerStrategy
    {
        public void SaveProductList(Dictionary<int, Product> productCatalogue)
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
            using (StreamWriter productListWriter = new($"{FileManagerOperations.CreateProductListFilePath()}", append: false))
            {
                productListWriter.Write(productString);
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
        public Dictionary<int, Product> LoadProductList()
        {
            Dictionary<int, Product> products = new Dictionary<int, Product>();
            if (File.Exists(FileManagerOperations.CreateProductListFilePath()))
            {
                var productListInfo = File.ReadAllLines(FileManagerOperations.CreateProductListFilePath());

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