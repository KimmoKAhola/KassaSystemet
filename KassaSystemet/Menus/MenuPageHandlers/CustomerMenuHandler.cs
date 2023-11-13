using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Models;
using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.Menus.MenuPages;

namespace KassaSystemet.Menus.MenuPageHandlers
{
    public class CustomerMenuHandler : IMenuHandler<CustomerMenuEnum>
    {
        private ShoppingCart _shoppingCart;
        public CustomerMenuHandler()
        {
            if (_shoppingCart == null || _shoppingCart.Purchases.Count == 0)
                _shoppingCart = new ShoppingCart();
        }

        public void HandleCustomerMenuOption(CustomerMenuEnum userInput, IFileManager fileManagerStrategy, IUserInputHandler userInputHandler)
        {
            switch (userInput)
            {
                case CustomerMenuEnum.First:
                    _shoppingCart.DisplayPurchases();
                    break;
                case CustomerMenuEnum.Second:
                    _shoppingCart.AddProductToCart(userInputHandler);
                    break;
                case CustomerMenuEnum.Third:
                    ProductCatalogue.Instance.DisplayProducts();
                    break;
                case CustomerMenuEnum.Pay:
                    if (_shoppingCart.Purchases.Count > 0)
                    {
                        string receipt = _shoppingCart.Pay();
                        fileManagerStrategy.SaveReceiptToFile(receipt);
                    }
                    else
                        Console.WriteLine("Your shopping cart is empty.");
                    break;
                case CustomerMenuEnum.Exit:
                    Console.WriteLine("Returning to the main menu.");
                    break;
                default:
                    Console.WriteLine("Invalid input.", Console.ForegroundColor = ConsoleColor.Red);
                    Thread.Sleep(1000);
                    Console.ResetColor();
                    break;
            }
            Console.ResetColor();
            Console.Write("Press any key to continue: ");
            Console.ReadKey();
        }

        public void HandleMenuOption(CustomerMenuEnum menuOption)
        {
            if (
        }
    }
}
