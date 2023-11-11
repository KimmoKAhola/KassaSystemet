using KassaSystemet;
using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.MenuPageServices
{
    public enum MenuHandlerEnum
    {
        first = 1,
        second,
        third,
        exit
    }
    public class StartMenuHandler : IMenuHandler
    {
        private MenuFactory _menuFactory;
        private IMenuHandler _menu;
        private MenuHandlerEnum _MenuHandlerEnum;
        private Dictionary<MenuHandlerEnum, string> _menuDisplayNames = new Dictionary<MenuHandlerEnum, string>()
        {
            {MenuHandlerEnum.first, "Customer Menu." },
            {MenuHandlerEnum.second, "Admin Menu." },
            {MenuHandlerEnum.third, "Info Menu." },
            {MenuHandlerEnum.exit, "Save & Exit." },
        };
        public StartMenuHandler(MenuFactory menuFactory)
        {
            _menuFactory = menuFactory;
        }
        public void InitializeMenu()
        {
            MenuHandlerEnum userInput;
            do
            {
                DisplayMenu();
                userInput = UserInputHandler.GetUserEnum();
                MenuHandler(userInput);
            } while (userInput != MenuHandlerEnum.exit);
        }
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option below.");
            foreach (var item in _menuDisplayNames)
            {
                Console.WriteLine($"{(int)item.Key}. {item.Value}");
            }
        }
        public void MenuHandler(MenuHandlerEnum menuHandlerEnum)
        {
            switch (menuHandlerEnum)
            {
                case MenuHandlerEnum.first:
                    _menu = _menuFactory.CreateMenu(MenuFactoryEnum.CustomerMenu);
                    _menu.InitializeMenu();
                    break;
                case MenuHandlerEnum.second:
                    _menu = _menuFactory.CreateMenu(MenuFactoryEnum.AdminMenu);
                    _menu.InitializeMenu();
                    break;
                case MenuHandlerEnum.third:
                    _menu = _menuFactory.CreateMenu(MenuFactoryEnum.InfoMenu);
                    _menu.InitializeMenu();
                    break;
                case MenuHandlerEnum.exit:
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