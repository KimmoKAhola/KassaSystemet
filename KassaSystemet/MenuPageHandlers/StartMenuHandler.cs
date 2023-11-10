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
    public class StartMenuHandler : IMenu
    {
        private readonly MenuFactory _menuFactory;
        private IMenu _menu;
        private IFileManager _fileManagerStrategy;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public StartMenuHandler(MenuFactory menuFactory, IFileManager fileManagerStrategy)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            _menuFactory = menuFactory;
            _fileManagerStrategy = fileManagerStrategy;
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
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                userInput = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
                MenuHandler(userInput, _fileManagerStrategy);
#pragma warning restore CS8604 // Possible null reference argument.
            } while (userInput != "0");
        }
        public void MenuHandler(string userInput, IFileManager fileManagerStrategy)
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