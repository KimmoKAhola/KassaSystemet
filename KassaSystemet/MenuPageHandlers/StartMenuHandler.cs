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
    public class StartMenuHandler : IMenuHandler
    {
        private readonly MenuFactory _menuFactory;
        private IMenuHandler _menu;
        public StartMenuHandler(MenuFactory menuFactory)
        {
            _menuFactory = menuFactory;
        }
        public void InitializeMenu()
        {
            string userInput;
            do
            {
                Console.Clear();
                Console.WriteLine("***Menu for the cash register***");
                Console.WriteLine("Choose an option below.");
                Console.WriteLine("1. New customer");
                Console.WriteLine("2. Admin tools");
                Console.WriteLine("0. Save & Exit.");
                Console.Write("Enter your command: ");
                userInput = Console.ReadLine();
                MenuHandler(userInput);
            } while (userInput != "0");
        }
        public void MenuHandler(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    _menu = _menuFactory.CreateMenu("Customer Menu");
                    _menu.InitializeMenu();
                    break;
                case "2":
                    _menu = _menuFactory.CreateMenu("Admin Menu");
                    _menu.InitializeMenu();
                    break;
                case "0":
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