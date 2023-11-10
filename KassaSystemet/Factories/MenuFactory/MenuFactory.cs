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
        private IFileManager _fileManagerStrategy;
        public MenuFactory(IFileManager fileManagerStrategy)
        {
            _fileManagerStrategy = fileManagerStrategy;
        }
        public IMenuHandler CreateMenu(string menuType)
        {
            switch (menuType)
            {
                case "Customer Menu":
                    return new CustomerMenu(_fileManagerStrategy);
                case "Admin Menu":
                    return new AdminMenu(_fileManagerStrategy);
                default:
                    return null;
            }
        }
    }
}
