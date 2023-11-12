using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using KassaSystemet.Strategy;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public class FileManager : IFileManager
    {
        private IFileManager _fileManagerStrategy;
        public FileManager(IFileManager fileManagerStrategy)
        {
            _fileManagerStrategy = fileManagerStrategy;
        }
        public void SaveReceiptToFile(string paymentInfo) => _fileManagerStrategy.SaveReceiptToFile(paymentInfo);
        public void SaveProductCatalogueToFile() => _fileManagerStrategy.SaveProductCatalogueToFile();
        public void SaveDiscountCatalogueToFile() => _fileManagerStrategy.SaveDiscountCatalogueToFile();
        public Dictionary<int, Product> LoadProductListFromFile() => _fileManagerStrategy.LoadProductListFromFile();
        public void LoadDiscountListFromFile() => _fileManagerStrategy.LoadDiscountListFromFile();
        public string LoadInfoMenuFromFile() => _fileManagerStrategy.LoadInfoMenuFromFile();
    }
}