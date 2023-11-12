using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KassaSystemet.File_IO
{
    public class SaveFileToJson : ISave
    {
        private Dictionary<int, Product> _productCatalogue = ProductCatalogue.Instance.Products;
        public void SaveDiscountListToFile()
        {
            throw new NotImplementedException();
        }

        public void SaveProductCatalogueToFile()
        {
            string jsonString = JsonSerializer.Serialize(_productCatalogue, new JsonSerializerOptions { WriteIndented = true });
            string _productListFolderPath = $"../../../Files/ProductLists";
            string filePath = $"{_productListFolderPath}/PRODUCT_LIST_ADMIN.json";
            // Write the JSON string to the specified file
            File.WriteAllText(filePath, jsonString);
        }

        public void SaveReceiptToFile(string paymentInfo)
        {
            throw new NotImplementedException();
        }
    }
}
