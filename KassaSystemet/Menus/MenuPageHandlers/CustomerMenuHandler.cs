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
    public class CustomerMenuHandler
    {
        private List<Purchase> _shoppingCart;
        public CustomerMenuHandler()
        {
            _shoppingCart = new List<Purchase>();
        }

        public void HandleCustomerMenuOption(CustomerMenuEnum userInput, IFileManager fileManagerStrategy, IUserInputHandler userInputHandler)
        {
            switch (userInput)
            {
                case CustomerMenuEnum.First:
                    Purchase.DisplayPurchases(_shoppingCart);
                    break;
                case CustomerMenuEnum.Second:
                    AddProduct(_shoppingCart, userInputHandler);
                    break;
                case CustomerMenuEnum.Third:
                    ProductCatalogue.Instance.DisplayProducts();
                    break;
                case CustomerMenuEnum.Pay:
                    string receipt = Purchase.Pay(_shoppingCart);
                    fileManagerStrategy.SaveReceiptToFile(receipt);
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
        private static void AddProduct(List<Purchase> shoppingCart, IUserInputHandler userInputHandler)
        {
            (int id, decimal amount) = userInputHandler.ProductInput();
            if (amount > 100)
                Console.WriteLine($"You can not purchase more than {100} of a product!", ConsoleColor.Red);
            else if (ProductCatalogue.Instance.Products.ContainsKey(id))
                shoppingCart.Add(ModelFactory.CreatePurchase(id, amount));
            else
                Console.WriteLine($"No product with id {id} exist in the system.", ConsoleColor.Red);
        }
    }
}
