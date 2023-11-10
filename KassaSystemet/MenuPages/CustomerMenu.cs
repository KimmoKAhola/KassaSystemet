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
    public class CustomerMenu : IMenu
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static CustomerMenuHandler _customerMenuHandler;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private readonly IFileManager _fileManagerStrategy;
        public CustomerMenu(IFileManager fileManagerStrategy)
        {
            _customerMenuHandler ??= new CustomerMenuHandler();
            _fileManagerStrategy = fileManagerStrategy;
        }

        public void InitializeMenu()
        {
            string userInput;
            do
            {
                Console.Clear();
                Console.WriteLine("****Welcome to the customer menu****");
                Console.WriteLine("1. Display your current cart.");
                Console.WriteLine("2. Enter products to purchase.");
                Console.WriteLine("3. Display available products.");
                Console.WriteLine("0: Clear shoppingcart and return to main menu.");
                Console.WriteLine("PAY: purchase wares in your cart and exit.");
                Console.Write("Enter command: ");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                userInput = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
                _customerMenuHandler.HandleCustomerMenuOption(userInput, _fileManagerStrategy);
#pragma warning restore CS8604 // Possible null reference argument.
            } while (userInput != "0");
        }
    }
}