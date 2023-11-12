using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Interfaces
{
    public interface IFolder
    {
        string GetReceiptFolderPath();
        string GetProductListFolderPath();
        string GetDiscountListFolderPath();
    }
}
