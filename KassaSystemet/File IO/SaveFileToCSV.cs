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
            using (StreamWriter productListWriter = new($"{GetProductListFolderPath()}.csv", append: false))
            {
                productListWriter.Write(productString);
            }
        }
        public void SaveDiscountCatalogueToFile()
        {
            var csvLines = ProductCatalogue.Instance.GetAllDiscounts().ToList();

            using (StreamWriter discountListWriter = new($"{GetDiscountListFolderPath()}.csv", append: false))
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
            throw new NotImplementedException();
        }

        public string GetReceiptFolderPath() => $"{_receiptsFolderPath}";
        public string GetProductListFolderPath() => $"{_productListFolderPath}/Product_LIST_ADMIN";

        public string GetDiscountListFolderPath() => $"{_discountListFolderPath}/Discount_LIST_ADMIN";
    }
}
