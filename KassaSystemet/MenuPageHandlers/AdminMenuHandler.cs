using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.Models;

namespace KassaSystemet.MenuPageServices
{
    public class AdminMenuHandler
    {
        private ProductCatalogue productCatalogue = ProductCatalogue.Instance;
        private ModelFactory _modelFactory;
        public AdminMenuHandler()
        {
        }
        public bool HandleAdminMenuOption(string userInput)
        {
            bool _isChanged = false;
            switch (userInput)
            {
                case "1":
                    AddNewProduct(ref _isChanged);
                    break;
                case "2":
                    DisplayAvailableProducts();
                    break;
                case "3":
                    ChangeProductPrice(ref _isChanged);
                    break;
                case "4":
                    ChangeProductName(ref _isChanged);
                    break;
                case "5":
                    AddProductDiscount(ref _isChanged);
                    break;
                case "6":
                    DisplayProductDiscount();
                    break;
                case "7":
                    DisplayAllDiscounts();
                    break;
                case "8":
                    RemoveProductDiscount(ref _isChanged);
                    break;
                case "9":
                    RemoveProduct(ref _isChanged);
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
        private void AddNewProduct(ref bool isChanged)
        {
            int productId = UserInputHandler.ProductIdInput();
            if (!productCatalogue.Products.ContainsKey(productId))
            {
                productCatalogue.AddNewProduct(productId);
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
        private void ChangeProductPrice(ref bool isChanged)
        {
            int productId = UserInputHandler.ProductIdInput();
            if (productCatalogue.Products.ContainsKey(productId))
            {
                productCatalogue.Products[productId].ChangeProductPrice();
                isChanged = true;
            }
            else
                Console.WriteLine($"The product id {productId} does not exist.", Console.ForegroundColor = ConsoleColor.Red);
        }
        private void ChangeProductName(ref bool isChanged)
        {
            int productId = UserInputHandler.ProductIdInput();
            if (productCatalogue.Products.ContainsKey(productId))
            {
                productCatalogue.Products[productId].ChangeProductName();
                isChanged = true;
            }
            else
                Console.WriteLine($"The product id {productId} does not exist.", Console.ForegroundColor = ConsoleColor.Red);
        }
        private void AddProductDiscount(ref bool isChanged)
        {
            int productId = UserInputHandler.ProductIdInput();

            if (productCatalogue.Products.ContainsKey(productId))
            {
                productCatalogue.AddNewDiscount(productId);
                isChanged = true;
            }
            else
                Console.WriteLine($"The product id {productId} does not exist.", Console.ForegroundColor = ConsoleColor.Red);
        }
        private void DisplayProductDiscount()
        {
            int productId = UserInputHandler.ProductIdInput();
            if (productCatalogue.ContainsDiscount(productId))
                productCatalogue.Products[productId].Display();
            else
            {
                Console.WriteLine($"The product id {productId} does not have a discount available.", Console.ForegroundColor = ConsoleColor.Red);
            }
        }
        private static void DisplayAllDiscounts() => ProductCatalogue.DisplayAllDiscounts();
        private void RemoveProductDiscount(ref bool isChanged)
        {
            int productId = UserInputHandler.ProductIdInput();
            if (productCatalogue.ContainsDiscount(productId))
            {
                productCatalogue.Products[productId].RemoveDiscount();
                isChanged = true;
            }
            else
                Console.WriteLine($"The product id {productId} does not have a discount available.", Console.ForegroundColor = ConsoleColor.Red);
        }
        private void RemoveProduct(ref bool isChanged)
        {
            int productId = UserInputHandler.ProductIdInput();
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