using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.File_IO
{
    public class SaveFileToCSV : ISave, IFolder
    {
        private static string _receiptsFolderPath = $"../../../Files/Receipts";
        private static string _productListFolderPath = $"../../../Files/ProductLists";
        private static string _discountListFolderPath = $"../../../Files/ProductLists";
        private static string FormatProductCatalogueToFile()
        {
            var csvLines = ProductCatalogue.Instance.Products.OrderBy(x => x.Key).Select(item =>
            $"{item.Key},{item.Value.ProductName},{item.Value.UnitPrice},{item.Value.PriceType}");

            return string.Join("\n", csvLines);
        }
        public void SaveProductCatalogueToFile()
        {
            string productString = FormatProductCatalogueToFile();
            using (StreamWriter productListWriter = new($"{CreateProductListFilePath()}.csv", append: false))
            {
                productListWriter.Write(productString);
            }
        }
        public void SaveDiscountCatalogueToFile()
        {
            var csvLines = ProductCatalogue.Instance.GetAllDiscounts().ToList();

            using (StreamWriter discountListWriter = new($"{CreateDiscountListFolderPath()}", append: false))
            {
                foreach (var product in csvLines)
                {
                    discountListWriter.WriteLine($"{product.ProductName}");
                    discountListWriter.WriteLine(string.Join("\n", product.Discounts.Select(d => d.ToString())));
                }
            }
        }
        public void SaveReceiptToFile(string paymentInfo)
        {
            using (StreamWriter receiptListWriter = new($"{CreateReceiptFilePath()}", append: false))
            {
                receiptListWriter.Write(paymentInfo);
            }
        }
        public string CreateReceiptFolderPath() => $"{_receiptsFolderPath}";
        public string CreateProductListFolderPath() => $"{_productListFolderPath}/Product_LIST_ADMIN";
        public string CreateDiscountListFolderPath() => $"{_discountListFolderPath}/Discount_LIST_ADMIN";

        public string CreateReceiptFilePath() => Path.Combine(_receiptsFolderPath, $"RECEIPT_{DateTime.Now.ToString("yyyyMMdd")}.csv");
        public string CreateReceiptIDFilePath() => Path.Combine(_receiptsFolderPath, $"RECEIPT_ID.csv");
        public string CreateDiscountListFilePath() => Path.Combine(_discountListFolderPath, $"DISCOUNT_LIST_ADMIN.csv");

        public string CreateProductListFilePath() => Path.Combine(_productListFolderPath, $"PRODUCT_LIST_ADMIN.csv");
    }
}
