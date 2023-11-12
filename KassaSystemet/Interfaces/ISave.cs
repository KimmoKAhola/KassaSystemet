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
        void SaveDiscountListToFile();
        void SaveReceiptToFile(string paymentInfo);
        void SaveProductCatalogueToFile();
    }
}