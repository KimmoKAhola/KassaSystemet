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
    public class AdminMenu : IMenu
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static AdminMenuHandler adminMenuHandler;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private readonly IFileManager _fileManagerStrategy;
        public AdminMenu(IFileManager strategy)
        {
            _fileManagerStrategy = strategy;
            adminMenuHandler ??= new AdminMenuHandler();
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
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                userInput = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8604 // Possible null reference argument.
                bool isChanged = adminMenuHandler.HandleAdminMenuOption(userInput);
#pragma warning restore CS8604 // Possible null reference argument.
                if (isChanged)
                {
                    _fileManagerStrategy.SaveProductCatalogue(ProductCatalogue.Instance.Products);
                    _fileManagerStrategy.SaveDiscountList(ProductCatalogue.Instance.Products);
                }
                Console.ResetColor();
                Console.Write("Press any key to continue: ");
                Console.ReadKey();

            } while (userInput != "0");
        }
    }
}