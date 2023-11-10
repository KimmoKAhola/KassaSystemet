using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Interfaces
{
    public interface IFileManagerStrategy
    {
        void SaveProductList(Dictionary<int, Product> productCatalogue);
        void SaveDiscountList(Dictionary<int, Product> productCatalogue);
        Dictionary<int, Product> LoadProductList();
        void LoadDiscountList(Dictionary<int, Product> productCatalogue);
        void SaveReceipt(string paymenyInfo);
    }
}