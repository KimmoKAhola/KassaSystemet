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
    public class AdminMenu : IMenuHandler
    {
        private static AdminMenuHandler adminMenuHandler;
        private IFileManager _fileManagerStrategy;
        private IUserInputHandler _userInputHandler;
        public AdminMenu(IFileManager strategy, IUserInputHandler userInputHandler)
        {
            _fileManagerStrategy = strategy;
            _userInputHandler = userInputHandler;
            adminMenuHandler ??= new AdminMenuHandler(_userInputHandler);
        }
        public void InitializeMenu()
        {
            string userInput;
            do
            {
                Console.Clear();
                Console.WriteLine("Admin menu");
                Console.WriteLine("1. Add a new product to the system\n" +
                    "2. Display available products in the system\n" +
                    "3. Change price on a product\n" +
                    "4. Change name on a product\n" +
                    "5. Add a discount for a product\n" +
                    "6. Display all available discounts for a single product\n" +
                    "7. Display all available discounts in the system\n" +
                    "8. Remove a discount from a product.\n" +
                    "9. Remove a product from the product list\n" +
                    "0. Exit admin menu");
                Console.Write("Enter a command: ");
                userInput = Console.ReadLine();
                bool isChanged = adminMenuHandler.HandleAdminMenuOption(userInput, _userInputHandler);
                if (isChanged)
                {
                    GetSaveFormat(_fileManagerStrategy);
                    _fileManagerStrategy.SaveProductCatalogueToTextFile(ProductCatalogue.Instance.Products);
                    _fileManagerStrategy.SaveDiscountList(ProductCatalogue.Instance.Products);
                }
                Console.ResetColor();
                Console.Write("Press any key to continue: ");
                Console.ReadKey();

            } while (userInput != "0");
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