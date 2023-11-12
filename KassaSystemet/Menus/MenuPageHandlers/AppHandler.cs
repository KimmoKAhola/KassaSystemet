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
        PersonalMenu,
        Exit
    }
    public class AppHandler : IMenu
    {
        public AppHandler(MenuFactory menuFactory)
        {
            _menuFactory = menuFactory;
        }
        private MenuFactory _menuFactory;
        private IMenu _menu;
        private Dictionary<StartMenuEnum, string> _startMenu = new Dictionary<StartMenuEnum, string>()
        {
            {StartMenuEnum.CustomerMenu, "Customer Menu." },
            {StartMenuEnum.AdminMenu, "Admin Menu." },
            {StartMenuEnum.InfoMenu, "Info Menu." },
            {StartMenuEnum.PersonalMenu, "Personal Menu." },
            {StartMenuEnum.Exit, "Save & Exit." },
        };

        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option below.");
            foreach (var item in _startMenu)
            {
                Console.WriteLine($"{(int)item.Key}. {item.Value}");
            }
        }

        public void InitializeMenu()
        {
            StartMenuEnum userInput;
            do
            {
                DisplayMenu();
                userInput = UserInputHandler.GetStartMenuEnum();
                MenuHandler(userInput);
            } while (userInput != StartMenuEnum.Exit);
        }
        public void MenuHandler(StartMenuEnum menuHandlerEnum)
        {
            switch (menuHandlerEnum)
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
                case StartMenuEnum.PersonalMenu:
                    _menu = _menuFactory.CreateMenu(MenuFactoryEnum.PersonalMenu);
                    _menu.InitializeMenu();
                    break;
                case StartMenuEnum.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    PrintErrorMessage("Invalid input.");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                    break;
            }
        }
        private void PrintErrorMessage(string message) => Console.WriteLine(message);
    }
}