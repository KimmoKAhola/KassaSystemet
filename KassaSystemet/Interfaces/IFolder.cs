using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Interfaces
{
    public interface IFolder
    {
        string CreateReceiptFolderPath();
        string CreateProductListFolderPath();
        string CreateDiscountListFolderPath();
        string CreateReceiptFilePath();
        string CreateReceiptIDFilePath();
        string CreateDiscountListFilePath();
        string CreateProductListFilePath();
    }
}
