using KassaSystemet.Interfaces;
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
        public string GetDiscountListFolderPath() => $"{_discountListFolderPath}/DISCOUNT_LIST_ADMIN";
        public string GetProductListFolderPath() => $"{_productListFolderPath}/Product_LIST_ADMIN";
        public string GetReceiptFolderPath() => $"{_receiptsFolderPath}/BinaryReceipt";

        public void SaveDiscountCatalogueToFile()
        {
            throw new NotImplementedException();
        }

        public void SaveProductCatalogueToFile()
        {
            throw new NotImplementedException();
        }

        public void SaveReceiptToFile(string paymentInfo)
        {
            string filePath = GetReceiptFolderPath();
            using(BinaryWriter bw = new BinaryWriter(File.Open(filePath, FileMode.Create)))
            {
                bw.Write(paymentInfo);
            }
        }
    }
}
