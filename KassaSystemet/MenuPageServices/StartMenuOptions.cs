using KassaSystemet;
using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.MenuPageServices
{
    public class StartMenuOptions : IMenu
    {
        private readonly MenuFactory _menuFactory;
        private IMenu _menu;

        public StartMenuOptions(MenuFactory menuFactory)
        {
            _menuFactory = menuFactory;
        }
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("***Menu for the cash register***");
            Console.WriteLine("Choose an option below.");
            Console.WriteLine("1. New customer");
            Console.WriteLine("2. Admin tools");
            Console.WriteLine("0. Save & Exit.");
            Console.Write("Enter your command: ");
            string userInput = Console.ReadLine();
            StartMenuHandler(userInput);
        }

        public void StartMenuHandler(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    //menuFactory = new MenuFactory("Customer Menu");
                    _menu = _menuFactory.CreateMenu("Customer Menu");
                    _menu.DisplayMenu();
                    //CustomerMenu(fileManager);
                    break;
                case "2":
                    //menuFactory = new MenuFactory("Admin Menu");
                    _menu = _menuFactory.CreateMenu("Admin Menu");
                    _menu.DisplayMenu();
                    //AdminMenu(fileManager);
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


//do
//{
//    string menuOption = Console.ReadLine();
//    {
//        switch (menuOption)
//        {
//            case "1":
//                //menuFactory = new MenuFactory("Customer Menu");
//                //CustomerMenu(fileManager);
//                break;
//            case "2":
//                //menuFactory = new MenuFactory("Admin Menu");
//                //AdminMenu(fileManager);
//                break;
//            case "0":
//                App.CloseApp();
//                break;
//            default:
//                Console.ForegroundColor = ConsoleColor.Red;
//                Console.WriteLine("Invalid input.");
//                Thread.Sleep(1000);
//                Console.ResetColor();
//                break;
//        }
//    }
//} while (true) ;