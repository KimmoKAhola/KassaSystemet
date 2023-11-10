using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Interfaces
{
    public interface ISave
    {
        void SaveProductCatalogueToTextFile(Dictionary<int, Product> productCatalogue);
        void SaveDiscountList(Dictionary<int, Product> productCatalogue);
        void SaveReceipt(string paymentInfo);
        public void SaveProductCatalogueToCsvFile(Dictionary<int, Product> productCatalogue);
    }
}