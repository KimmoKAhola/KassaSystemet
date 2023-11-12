using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.File_IO
{
    public class SaveFileToCSV : ISave
    {
        private static string FormatProductCatalogueToFile()
        {
            var csvLines = ProductCatalogue.Instance.Products.OrderBy(x => x.Key).Select(item =>
            $"{item.Key},{item.Value.ProductName},{item.Value.UnitPrice},{item.Value.PriceType}");

            return string.Join("\n", csvLines);
        }

        public void SaveProductCatalogueToFile()
        {
            string productString = FormatProductCatalogueToFile();
            //productString = productString.Substring(0, productString.Length - 1);
            using (StreamWriter productListWriter = new($"{FileManagerOperations.CreateProductListFilePathCsv()}", append: false))
            {
                productListWriter.Write(productString);
            }
        }
        public void SaveDiscountListToFile()
        {
            throw new NotImplementedException();
        }

        public void SaveReceiptToFile(string paymentInfo)
        {
            throw new NotImplementedException();
        }
    }
}
