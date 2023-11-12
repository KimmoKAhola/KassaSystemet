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
    public class SaveFileToJson : ISave, IFolder
    {
        private Dictionary<int, Product> _productCatalogue = ProductCatalogue.Instance.Products;
        private static string _receiptsFolderPath = $"../../../Files/Receipts";
        private static string _productListFolderPath = $"../../../Files/ProductLists";
        public static string GetDiscountListFilePath() => $"{_productListFolderPath}/DISCOUNT_LIST_ADMIN";
        public string GetProductListFolderPath() => $"{_productListFolderPath}/Product_LIST_ADMIN";
        public string GetReceiptFolderPath() => $"{_receiptsFolderPath}";

        public void SaveDiscountCatalogueToFile()
        {
            var discount = ProductCatalogue.Instance.GetAllDiscounts().ToString();
            string filePath = $"{GetDiscountListFilePath()}.json";
            File.WriteAllText(filePath, discount);
        }

        public void SaveProductCatalogueToFile()
        {
            string jsonString = JsonSerializer.Serialize(_productCatalogue, new JsonSerializerOptions { WriteIndented = true });
            string filePath = $"{GetProductListFolderPath()}.json";
            File.WriteAllText(filePath, jsonString);
        }

        public void SaveReceiptToFile(string paymentInfo)
        {
            throw new NotImplementedException();
        }

        public string GetDiscountListFolderPath()
        {
            throw new NotImplementedException();
        }
    }
}
