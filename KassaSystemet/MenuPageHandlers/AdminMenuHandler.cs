using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.Models;

namespace KassaSystemet.MenuPageServices
{
    public class AdminMenuHandler
    {
        private ProductCatalogue productCatalogue = ProductCatalogue.Instance;

        public AdminMenuHandler()
        {
        }
        public bool HandleAdminMenuOption(string userInput, IUserInputHandler userInputHandler)
        {
            bool _isChanged = false;
            switch (userInput)
            {
                case "1":
                    AddNewProduct(ref _isChanged, userInputHandler);
                    break;
                case "2":
                    DisplayAvailableProducts();
                    break;
                case "3":
                    ChangeProductPrice(ref _isChanged, userInputHandler);
                    break;
                case "4":
                    ChangeProductName(ref _isChanged, userInputHandler);
                    break;
                case "5":
                    AddProductDiscount(ref _isChanged, userInputHandler);
                    break;
                case "6":
                    DisplayProductDiscount(userInputHandler);
                    break;
                case "7":
                    DisplayAllDiscounts();
                    break;
                case "8":
                    RemoveProductDiscount(ref _isChanged, userInputHandler);
                    break;
                case "9":
                    RemoveProduct(ref _isChanged, userInputHandler);
                    break;
                case "0":
                    Console.WriteLine("Return to the main menu.");
                    break;
                default:
                    Console.WriteLine("Invalid input.", Console.ForegroundColor = ConsoleColor.Red);
                    Thread.Sleep(1000);
                    Console.ResetColor();
                    break;
            }
            return _isChanged;
        }
        private void AddNewProduct(ref bool isChanged, IUserInputHandler userInputHandler)
        {
            int productId = userInputHandler.ProductIdInput();
            if (!productCatalogue.Products.ContainsKey(productId))
            {
                productCatalogue.AddNewProduct(productId, userInputHandler);
                isChanged = true;
            }
            else
                Console.WriteLine($"The product id {productId} already exists.", Console.ForegroundColor = ConsoleColor.Red);
        }
        private void DisplayAvailableProducts()
        {
            Console.WriteLine("These are the available products in the system: ");
            productCatalogue.DisplayProducts();
        }
        private void ChangeProductPrice(ref bool isChanged, IUserInputHandler userInputHandler)
        {
            int productId = userInputHandler.ProductIdInput();
            if (productCatalogue.Products.ContainsKey(productId))
            {
                productCatalogue.Products[productId].ChangeProductPrice();
                isChanged = true;
            }
            else
                Console.WriteLine($"The product id {productId} does not exist.", Console.ForegroundColor = ConsoleColor.Red);
        }
        private void ChangeProductName(ref bool isChanged, IUserInputHandler userInputHandler)
        {
            int productId = userInputHandler.ProductIdInput();
            if (productCatalogue.Products.ContainsKey(productId))
            {
                productCatalogue.Products[productId].ChangeProductName();
                isChanged = true;
            }
            else
                Console.WriteLine($"The product id {productId} does not exist.", Console.ForegroundColor = ConsoleColor.Red);
        }
        private void AddProductDiscount(ref bool isChanged, IUserInputHandler userInputHandler)
        {
            int productId = userInputHandler.ProductIdInput();

            if (productCatalogue.Products.ContainsKey(productId))
            {
                productCatalogue.AddNewDiscount(productId, userInputHandler);
                isChanged = true;
            }
            else
                Console.WriteLine($"The product id {productId} does not exist.", Console.ForegroundColor = ConsoleColor.Red);
        }
        private void DisplayProductDiscount(IUserInputHandler userInputHandler)
        {
            int productId = userInputHandler.ProductIdInput();
            if (productCatalogue.ContainsDiscount(productId))
                productCatalogue.Products[productId].Display();
            else
            {
                Console.WriteLine($"The product id {productId} does not have a discount available.", Console.ForegroundColor = ConsoleColor.Red);
            }
        }
        private static void DisplayAllDiscounts() => ProductCatalogue.DisplayAllDiscounts();
        private void RemoveProductDiscount(ref bool isChanged, IUserInputHandler userInputHandler)
        {
            int productId = userInputHandler.ProductIdInput();
            if (productCatalogue.ContainsDiscount(productId))
            {
                productCatalogue.Products[productId].RemoveDiscount();
                isChanged = true;
            }
            else
                Console.WriteLine($"The product id {productId} does not have a discount available.", Console.ForegroundColor = ConsoleColor.Red);
        }
        private void RemoveProduct(ref bool isChanged, IUserInputHandler userInputHandler)
        {
            int productId = userInputHandler.ProductIdInput();
            if (productCatalogue.Products.ContainsKey(productId))
            {
                productCatalogue.RemoveProduct(productId);
                Console.WriteLine($"Removed the product with id {productId} from the system.", Console.ForegroundColor = ConsoleColor.Green);
                isChanged = true;
            }
            else
                Console.WriteLine($"The product id {productId} does not exist.", Console.ForegroundColor = ConsoleColor.Red);
        }
    }
}