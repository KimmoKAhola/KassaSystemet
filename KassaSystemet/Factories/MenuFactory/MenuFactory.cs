using KassaSystemet.Interfaces;
using KassaSystemet.MenuPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Factories.MenuFactory
{
    public class MenuFactory
    {
        public IMenu CreateMenu(string menuType)
        {
            switch (menuType)
            {
                case "Customer Menu":
                    return new CustomerMenu();
                case "Admin Menu":
                    return new AdminMenu();
                default:
                    return null;
            }
        }
    }
}
