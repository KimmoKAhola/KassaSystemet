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

    }
}
