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
        private IUserInputHandler _userInputHandler;
        public MenuFactory(IFileManager fileManagerStrategy, IUserInputHandler userInputHandler)
        {
            _fileManagerStrategy = fileManagerStrategy;
            _userInputHandler = userInputHandler;
        }
        public IMenuHandler CreateMenu(MenuFactoryEnum factoryEnum)
        {
            switch (factoryEnum)
            {
                case MenuFactoryEnum.CustomerMenu:
                    return new CustomerMenu(_fileManagerStrategy, _userInputHandler);
                case MenuFactoryEnum.AdminMenu:
                    return new AdminMenu(_fileManagerStrategy, _userInputHandler);
                case MenuFactoryEnum.InfoMenu:
                    return new InfoMenu(_fileManagerStrategy);
                default:
                    return null;
            }
        }
    }
}
