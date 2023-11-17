using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.File_IO
{
    public class SaveFileToBinary : ISave, IFolder
    {
        private static string _receiptsFolderPath = $"../../../Files/Receipts";
        private static string _productListFolderPath = $"../../../Files/ProductLists";
        private static string _discountListFolderPath = $"../../../Files/DiscountLists";

        public string CreateDiscountListFilePath() => Path.Combine(_discountListFolderPath, $"DISCOUNT_LIST_ADMIN");
        public string CreateReceiptFilePath() => Path.Combine(_receiptsFolderPath, $"RECEIPT_{DateTime.Now.ToString("yyyyMMdd")}");
        public string CreateReceiptIDFilePath() => Path.Combine(_receiptsFolderPath, $"RECEIPT_ID");
        public string CreateDiscountListFolderPath() => $"{_discountListFolderPath}/DISCOUNT_LIST_ADMIN";
        public string CreateProductListFolderPath() => $"{_productListFolderPath}/Product_LIST_ADMIN";
        public string CreateReceiptFolderPath() => $"{_receiptsFolderPath}/BinaryReceipt";
        public string CreateProductListFilePath() => $"{_productListFolderPath}/";


        public void SaveDiscountCatalogueToFile()
        {
            var discounts = ProductCatalogue.Instance.GetAllDiscounts().ToList();

            string filePath = CreateDiscountListFolderPath();
            using (BinaryWriter bw = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                foreach (var product in discounts)
                {
                    bw.Write($"{product.ProductName} has the discounts: \n");
                    bw.Write(string.Join("\n", product.Discounts.Select(d => d.ToString())));
                    bw.Write('\n');
                }
            }
        }

        public void SaveProductCatalogueToFile()
        {
            var products = ProductCatalogue.Instance.Products.ToList();
            var productString = new StringBuilder();
            foreach (var item in products)
            {
                productString = productString.AppendLine(item.ToString());

            }

            string filePath = CreateProductListFolderPath();
            using (BinaryWriter bw = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                bw.Write(productString.ToString());
            }
        }

        public void SaveReceiptToFile(string paymentInfo)
        {
            string filePath = CreateReceiptFolderPath();
            using (BinaryWriter bw = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                bw.Write(paymentInfo);
            }
        }

    }
}
