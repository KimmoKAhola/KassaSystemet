using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.MenuPageServices;
using KassaSystemet.Models;

namespace KassaSystemet.MenuPages
{
    public class StartMenu
    {
        private readonly MenuFactory _menuFactory;
        private IMenu _menu;
        public StartMenu(MenuFactory menuFactory)
        {
            _menuFactory = menuFactory;
        }

        public void Start(FileManager fileManager)
        {
            StartMenuOptions startMenuOptions = new StartMenuOptions(_menuFactory);
            startMenuOptions.DisplayMenu();
        }

        public void ChangeMenu(string menuType)
        {

        }

        public void StartMenuHandler(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    _menu = _menuFactory.CreateMenu("Customer Menu");
                    _menu.DisplayMenu();
                    break;
                case "2":
                    _menu = _menuFactory.CreateMenu("Admin Menu");
                    _menu.DisplayMenu();
                    break;
                case "0":
                    App.CloseApp();
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