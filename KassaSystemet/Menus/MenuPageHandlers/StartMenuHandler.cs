using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.File_IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Menus.MenuPages;
using KassaSystemet.Utilities;

namespace KassaSystemet.Menus.MenuPageHandlers
{
    public enum StartMenuEnum
    {
        CustomerMenu = 1,
        AdminMenu,
        InfoMenu,
        CreditMenu,
        Exit
    }
    public class StartMenuHandler : IMenuHandler<StartMenuEnum>
    {
        public StartMenuHandler(MenuFactory menuFactory)
        {
            _menuFactory = menuFactory;
        }
        MenuFactory _menuFactory;
        IMenu _menu;

        public void HandleMenuOption(StartMenuEnum menuOption)
        {
            switch (menuOption)
            {
                case StartMenuEnum.CustomerMenu:
                    _menu = _menuFactory.CreateMenu(MenuFactoryEnum.CustomerMenu);
                    _menu.InitializeMenu();
                    break;
                case StartMenuEnum.AdminMenu:
                    _menu = _menuFactory.CreateMenu(MenuFactoryEnum.AdminMenu);
                    _menu.InitializeMenu();
                    break;
                case StartMenuEnum.InfoMenu:
                    _menu = _menuFactory.CreateMenu(MenuFactoryEnum.InfoMenu);
                    _menu.InitializeMenu();
                    break;
                case StartMenuEnum.CreditMenu:
                    _menu = _menuFactory.CreateMenu(MenuFactoryEnum.CreditMenu);
                    _menu.InitializeMenu();
                    break;
                case StartMenuEnum.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    PrintErrorMessage("Invalid input.");
                    Thread.Sleep(1000);
                    break;
            }
        }
        private static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}