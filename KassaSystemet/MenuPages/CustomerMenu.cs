using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.MenuPageServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.MenuPages
{
    public class CustomerMenu : IMenuHandler
    {
        private static CustomerMenuHandler _customerMenuHandler;
        private IFileManager _fileManagerStrategy;
        private IUserInputHandler _userInputHandler;
        public CustomerMenu(IFileManager fileManagerStrategy, IUserInputHandler userInputHandler)
        {
            _fileManagerStrategy = fileManagerStrategy;
            _userInputHandler = userInputHandler;
            _customerMenuHandler ??= new CustomerMenuHandler();
        }

        public void InitializeMenu()
        {
            string userInput;
            do
            {
                DisplayMenu();
                userInput = Console.ReadLine();
                _customerMenuHandler.HandleCustomerMenuOption(userInput, _fileManagerStrategy, _userInputHandler);
            } while (userInput != "0");
        }
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("****Welcome to the customer menu****");
            Console.WriteLine("1. Display your current cart.");
            Console.WriteLine("2. Enter products to purchase.");
            Console.WriteLine("3. Display available products.");
            Console.WriteLine("0: Clear shoppingcart and return to main menu.");
            Console.WriteLine("PAY: purchase wares in your cart and exit.");
            Console.Write("Enter command: ");
        }
    }
}