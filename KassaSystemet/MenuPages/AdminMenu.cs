using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.MenuPageServices;
using KassaSystemet.Models;
using KassaSystemet.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.MenuPages
{
    public enum AdminMenuEnum
    {
        First = 1,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh,
        Eighth,
        Ninth,
        Exit
    }
    public class AdminMenu : IMenuHandler
    {
        public AdminMenu(IFileManager strategy, IUserInputHandler userInputHandler)
        {
            _fileManagerStrategy = strategy;
            _userInputHandler = userInputHandler;
            adminMenuHandler ??= new AdminMenuHandler();
        }
        private static AdminMenuHandler adminMenuHandler;
        private IFileManager _fileManagerStrategy;
        private IUserInputHandler _userInputHandler;
        private Dictionary<AdminMenuEnum, string> _adminMenuDisplayNames = new Dictionary<AdminMenuEnum, string>()
        {
            {AdminMenuEnum.First, "Add a new product to the system." },
            {AdminMenuEnum.Second, "Display available products in the system." },
            {AdminMenuEnum.Third, "Change a product's price." },
            {AdminMenuEnum.Fourth, "Change a product's name." },
            {AdminMenuEnum.Fifth, "Add a new discount to the system." },
            {AdminMenuEnum.Sixth, "Display all available discounts for a specific product." },
            {AdminMenuEnum.Seventh, "Display all available discounts in the system." },
            {AdminMenuEnum.Eighth, "Remove a specific discount from a product." },
            {AdminMenuEnum.Ninth, "Remove a product from the system." },
            {AdminMenuEnum.Exit, "Return to the main menu." },
        };
        public void InitializeMenu()
        {
            AdminMenuEnum userInput;
            do
            {
                DisplayMenu();
                userInput = _userInputHandler.GetAdminMenuEnum();
                bool isChanged = adminMenuHandler.HandleAdminMenuOption(userInput, _userInputHandler);
                if (isChanged)
                {
                    GetSaveFormat(_fileManagerStrategy);
                    _fileManagerStrategy.SaveProductCatalogueToTextFile(ProductCatalogue.Instance.Products);
                    _fileManagerStrategy.SaveDiscountList(ProductCatalogue.Instance.Products);
                }
            } while (userInput != AdminMenuEnum.Exit);
        }
        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("**Admin menu**\nChoose an option below.");
            foreach (var item in _adminMenuDisplayNames)
            {
                Console.WriteLine($"{(int)item.Key}. {item.Value}");
            }
        }

        private static void GetSaveFormat(IFileManager _fileManagerStrategy)
        {
            Console.WriteLine("Choose save format:");
            Console.WriteLine("1. TXT");
            Console.WriteLine("2. CSV");

            string userInput = Console.ReadLine();
            if (userInput == "1")
            {
                _fileManagerStrategy.SaveProductCatalogueToTextFile(ProductCatalogue.Instance.Products);
            }
            else
            {
                _fileManagerStrategy.SaveProductCatalogueToCsvFile(ProductCatalogue.Instance.Products); ;
            }
        }
    }
}