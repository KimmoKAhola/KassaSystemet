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
    public class FileManager : ISave, ILoad
    {
        private readonly IFileManagerStrategy _fileManagerStrategy;
        public FileManager(IFileManagerStrategy fileManagerStrategy)
        {
            _fileManagerStrategy = fileManagerStrategy;
        }
        public void SaveReceipt(string paymentInfo) => _fileManagerStrategy.SaveReceipt(paymentInfo);
        public void SaveProductList() => _fileManagerStrategy.SaveProductList(ProductCatalogue.Instance.Products);
        public void SaveDiscountList() => _fileManagerStrategy.SaveDiscountList(ProductCatalogue.Instance.Products);
        public Dictionary<int, Product> LoadProductList() => _fileManagerStrategy.LoadProductList();
        public void LoadDiscountList() => _fileManagerStrategy.LoadDiscountList(ProductCatalogue.Instance.Products);
    }
}