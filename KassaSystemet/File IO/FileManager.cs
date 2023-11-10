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
        private readonly IFileManager _fileManagerStrategy;
        public FileManager(IFileManager fileManagerStrategy)
        {
            _fileManagerStrategy = fileManagerStrategy;
        }
        public void SaveReceipt(string paymentInfo) => _fileManagerStrategy.SaveReceipt(paymentInfo);
        public void SaveProductCatalogueTextFile(Dictionary<int, Product> productCatalogue) => _fileManagerStrategy.SaveProductCatalogueTextFile(productCatalogue);
        public void SaveProductCatalogueCsvFile(Dictionary<int, Product> productCatalogue) => _fileManagerStrategy.SaveProductCatalogueTextFile(productCatalogue);
        public void SaveDiscountList(Dictionary<int, Product> productCatalogue) => _fileManagerStrategy.SaveDiscountList(productCatalogue);
        public Dictionary<int, Product> LoadProductList() => _fileManagerStrategy.LoadProductList();
        public void LoadDiscountList() => _fileManagerStrategy.LoadDiscountList();
    }
}