using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.Menus.MenuPageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Menus.MenuPages
{
    public enum CustomerMenuEnum
    {
        First = 1,
        Second,
        Third,
        Pay,
        Exit
    }
    public class CustomerMenu : IMenu
    {
        public CustomerMenu(IFileManager fileManagerStrategy, IUserInputHandler userInputHandler)
        {
            _fileManagerStrategy = fileManagerStrategy;
            _userInputHandler = userInputHandler;
            _customerMenuHandler ??= new CustomerMenuHandler(_fileManagerStrategy, _userInputHandler);
        }
        private static CustomerMenuHandler _customerMenuHandler;
        private IFileManager _fileManagerStrategy;
        private IUserInputHandler _userInputHandler;
        private Dictionary<CustomerMenuEnum, string> _customerMenu = new Dictionary<CustomerMenuEnum, string>()
        {
            {CustomerMenuEnum.First, "Display your current shopping cart." },
            {CustomerMenuEnum.Second, "Add a product to your shopping cart." },
            {CustomerMenuEnum.Third, "Display all available products in the system." },
            {CustomerMenuEnum.Pay, "Purchase all products in your shopping cart." },
            {CustomerMenuEnum.Exit, "Clear your current shopping cart and return to the main menu." },
        };
        public void InitializeMenu()
        {
            _fileManagerStrategy.LoadDiscountListFromFile();
            CustomerMenuEnum userInput;
            do
            {
                DisplayMenu();
                userInput = _userInputHandler.GetMenuEnum<CustomerMenuEnum>();
                _customerMenuHandler.HandleCustomerMenuOption(userInput);
            } while (userInput != CustomerMenuEnum.Exit);
        }
        public void DisplayMenu()
        {
            Console.Clear();
            PrintMessage("Welcome to the customer menu.\nChoose an option below.");
            foreach (var item in _customerMenu)
            {
                Console.WriteLine($"{(int)item.Key}. {item.Value}");
            }
        }
        private static void PrintMessage(string message) => Console.WriteLine(message);
    }
}