using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using KassaSystemet.Menus.MenuPages;

namespace KassaSystemet.Menus.MenuPageHandlers
{
    public class AdminMenuHandler
    {
        private ProductCatalogue productCatalogue = ProductCatalogue.Instance;
        public bool HandleAdminMenuOption(AdminMenuEnum adminMenuHandlerEnum, IUserInputHandler userInputHandler)
        {
            bool _isChanged = false;
            switch (adminMenuHandlerEnum)
            {
                case AdminMenuEnum.First:
                    AddNewProduct(ref _isChanged, userInputHandler);
                    break;
                case AdminMenuEnum.Second:
                    DisplayAvailableProducts();
                    break;
                case AdminMenuEnum.Third:
                    ChangeProductPrice(ref _isChanged, userInputHandler);
                    break;
                case AdminMenuEnum.Fourth:
                    ChangeProductName(ref _isChanged, userInputHandler);
                    break;
                case AdminMenuEnum.Fifth:
                    AddProductDiscount(ref _isChanged, userInputHandler);
                    break;
                case AdminMenuEnum.Sixth:
                    DisplayProductDiscount(userInputHandler);
                    break;
                case AdminMenuEnum.Seventh:
                    DisplayAllDiscounts();
                    break;
                case AdminMenuEnum.Eighth:
                    RemoveProductDiscount(ref _isChanged, userInputHandler);
                    break;
                case AdminMenuEnum.Ninth:
                    RemoveProduct(ref _isChanged, userInputHandler);
                    break;
                case AdminMenuEnum.Exit:
                    Console.WriteLine("Return to the main menu.");
                    break;
                default:
                    Console.WriteLine("Invalid input.", Console.ForegroundColor = ConsoleColor.Red);
                    Thread.Sleep(1000);
                    Console.ResetColor();
                    break;
            }
            Console.Write("Press any key to continue: ");
            Console.ReadKey();
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