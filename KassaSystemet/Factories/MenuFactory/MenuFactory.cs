using KassaSystemet.Interfaces;
using KassaSystemet.Menus.MenuPageHandlers;
using KassaSystemet.Menus.MenuPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Factories.MenuFactory
{
    public class MenuFactory
    {
        private IFileManager _fileManager;
        private IUserInputHandler _userInputHandler;
        public MenuFactory(IFileManager fileManager, IUserInputHandler userInputHandler)
        {
            _fileManager = fileManager;
            _userInputHandler = userInputHandler;
        }
        public IMenu CreateMenu(MenuFactoryEnum factoryEnum)
        {
            switch (factoryEnum)
            {
                case MenuFactoryEnum.CustomerMenu:
                    return new CustomerMenu(_fileManager, _userInputHandler);
                case MenuFactoryEnum.AdminMenu:
                    return new AdminMenu(_fileManager, _userInputHandler);
                case MenuFactoryEnum.InfoMenu:
                    return new InfoMenu(_fileManager);
                default:
                    return null;
            }
        }
    }
}
