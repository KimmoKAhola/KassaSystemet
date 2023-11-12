using KassaSystemet.Menus.MenuPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Interfaces
{
    public interface IUserInputHandler
    {
        (int, decimal) ProductInput();
        int ProductIdInput();
        (string, string, decimal) DiscountInput();
        (string, decimal, string) NewProduct();

        AdminMenuEnum GetAdminMenuEnum();
        CustomerMenuEnum GetCustomerMenuEnum();
        //StartMenuEnum GetStartMenuEnum();
        SaveFormatEnum GetSaveFormatEnum();
    }
}
