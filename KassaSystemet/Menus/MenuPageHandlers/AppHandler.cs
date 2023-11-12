using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.Strategy;
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
        First = 1,
        Second,
        Third,
        Exit
    }
    public class AppHandler : IMenu
    {
        public AppHandler(MenuFactory menuFactory)
        {
            _menuFactory = menuFactory;
        }
        private MenuFactory _menuFactory;
        //private IFileManager _fileManager;
        //private IUserInputHandler _userInputHandler;
        private IMenu _menu;
        private Dictionary<StartMenuEnum, string> _startMenu = new Dictionary<StartMenuEnum, string>()
        {
            {StartMenuEnum.First, "Customer Menu." },
            {StartMenuEnum.Second, "Admin Menu." },
            {StartMenuEnum.Third, "Info Menu." },
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
                case StartMenuEnum.First:
                    _menu = _menuFactory.CreateMenu(MenuFactoryEnum.CustomerMenu);
                    _menu.InitializeMenu();
                    break;
                case StartMenuEnum.Second:
                    _menu = _menuFactory.CreateMenu(MenuFactoryEnum.AdminMenu);
                    _menu.InitializeMenu();
                    break;
                case StartMenuEnum.Third:
                    _menu = _menuFactory.CreateMenu(MenuFactoryEnum.InfoMenu);
                    _menu.InitializeMenu();
                    break;
                case StartMenuEnum.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input.");
                    Thread.Sleep(1000);
                    Console.ResetColor();
                    break;
            }
        }
    }
}