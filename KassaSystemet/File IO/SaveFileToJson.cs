using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace KassaSystemet.File_IO
{
    public class SaveFileToJson : ISave, IFolder
    {
        private Dictionary<int, Product> _productCatalogue = ProductCatalogue.Instance.Products;
        private static string _receiptsFolderPath = $"../../../Files/Receipts";
        private static string _productListFolderPath = $"../../../Files/ProductLists";
        private static string _discountListFolderPath = $"../../../Files/DiscountLists";
        public static string GetDiscountListFilePath() => $"{_discountListFolderPath}/DISCOUNT_LIST_ADMIN";
        public string GetProductListFolderPath() => $"{_productListFolderPath}/Product_LIST_ADMIN";
        public string GetReceiptFolderPath() => $"{_receiptsFolderPath}";

        public void SaveDiscountCatalogueToFile()
        {
            var discount = ProductCatalogue.Instance.GetAllDiscounts().ToList();

            var discountJson = JsonSerializer.Serialize(discount, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            string filePath = $"{GetDiscountListFilePath()}.json";
            File.WriteAllText(filePath, discountJson);
        }

        public void SaveProductCatalogueToFile()
        {
            string jsonString = JsonSerializer.Serialize(_productCatalogue, new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
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

        public string CreateReceiptFilePath() => Path.Combine(_receiptsFolderPath, $"RECEIPT_{DateTime.Now.ToString("yyyyMMdd")}");
        public string CreateReceiptIDFilePath() => Path.Combine(_receiptsFolderPath, $"RECEIPT_ID");
        public string CreateDiscountListFilePath() => Path.Combine(_discountListFolderPath, $"DISCOUNT_LIST_ADMIN");
    }
}
