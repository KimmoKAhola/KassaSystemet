using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using KassaSystemet.Menus.MenuPages;
using System.Threading.Channels;

namespace KassaSystemet.Menus.MenuPageHandlers
{
    public class AdminMenuHandler : IMenuHandler<AdminMenuEnum>
    {
        public AdminMenuHandler(IUserInputHandler userInputHandler)
        {
            _userInputHandler = userInputHandler;
        }
        private IUserInputHandler _userInputHandler;
        private ProductCatalogue productCatalogue = ProductCatalogue.Instance;
        public void HandleMenuOption(AdminMenuEnum menuOption)
        {
            if (menuOption is AdminMenuEnum adminMenuEnum)
                HandleAdminMenuOption(adminMenuEnum);
        }
        public bool HandleAdminMenuOption(AdminMenuEnum adminMenuHandlerEnum)
        {
            bool _isChanged = false;
            switch (adminMenuHandlerEnum)
            {
                case AdminMenuEnum.First:
                    AddNewProduct(ref _isChanged, _userInputHandler);
                    break;
                case AdminMenuEnum.Second:
                    DisplayAvailableProducts();
                    break;
                case AdminMenuEnum.Third:
                    ChangeProductPrice(ref _isChanged, _userInputHandler);
                    break;
                case AdminMenuEnum.Fourth:
                    ChangeProductName(ref _isChanged, _userInputHandler);
                    break;
                case AdminMenuEnum.Fifth:
                    AddProductDiscount(ref _isChanged, _userInputHandler);
                    break;
                case AdminMenuEnum.Sixth:
                    DisplayProductDiscount(_userInputHandler);
                    break;
                case AdminMenuEnum.Seventh:
                    DisplayAllDiscounts();
                    break;
                case AdminMenuEnum.Eighth:
                    RemoveProductDiscount(ref _isChanged, _userInputHandler);
                    break;
                case AdminMenuEnum.Ninth:
                    RemoveProduct(ref _isChanged, _userInputHandler);
                    break;
                case AdminMenuEnum.Exit:
                    PrintSuccessMessage("Returning to the main menu.");
                    break;
                default:
                    PrintErrorMessage("Invalid input.");
                    Thread.Sleep(1000);
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
                PrintErrorMessage($"The product id {productId} already exists.");
        }
        private void DisplayAvailableProducts()
        {
            PrintSuccessMessage("These are the available products in the system: ");
            productCatalogue.DisplayProducts();
        }
        private void ChangeProductPrice(ref bool isChanged, IUserInputHandler userInputHandler)
        {
            int productId = userInputHandler.ProductIdInput();
            if (productCatalogue.Products.ContainsKey(productId))
            {
                decimal price = userInputHandler.GetValidProductPrice();
                productCatalogue.Products[productId].UnitPrice = price;
                PrintSuccessMessage($"Price has been changed to {price:C2}");
                isChanged = true;
            }
            else
                PrintErrorMessage($"The product id {productId} does not exist.");
        }
        private void ChangeProductName(ref bool isChanged, IUserInputHandler userInputHandler)
        {
            int productId = userInputHandler.ProductIdInput();
            if (productCatalogue.Products.ContainsKey(productId))
            {
                string name = userInputHandler.GetValidProductName();
                productCatalogue.Products[productId].ProductName = name;
                PrintSuccessMessage($"Name has been changed to {productCatalogue.Products[productId].ProductName}");
                isChanged = true;
            }
            else
                PrintErrorMessage($"The product id {productId} does not exist.");
        }
        private void AddProductDiscount(ref bool isChanged, IUserInputHandler userInputHandler)
        {
            int productId = userInputHandler.ProductIdInput();

            if (productCatalogue.Products.ContainsKey(productId))
            {
                productCatalogue.AddNewDiscount(productId, userInputHandler, out bool result);
                if (result)
                    isChanged = true;
            }
            else
                PrintErrorMessage($"The product id {productId} does not exist.");
        }
        private void DisplayProductDiscount(IUserInputHandler userInputHandler)
        {
            int productId = userInputHandler.ProductIdInput();
            if (productCatalogue.ContainsDiscount(productId))
                productCatalogue.Products[productId].Display();
            else
                PrintErrorMessage($"The product id {productId} does not have a discount available.");
        }
        private static void DisplayAllDiscounts() => ProductCatalogue.DisplayAllDiscounts();
        private void RemoveProductDiscount(ref bool isChanged, IUserInputHandler userInputHandler)
        {
            int productId = userInputHandler.ProductIdInput();
            if (productCatalogue.ContainsDiscount(productId))
            {
                productCatalogue.Products[productId].RemoveDiscount(out bool result);
                if (result)
                    isChanged = true;
            }
            else
                PrintErrorMessage($"The product id {productId} does not have a discount available.");
        }
        private void RemoveProduct(ref bool isChanged, IUserInputHandler userInputHandler)
        {
            int productId = userInputHandler.ProductIdInput();
            if (productCatalogue.Products.ContainsKey(productId))
            {
                productCatalogue.RemoveProduct(productId);
                PrintSuccessMessage($"Removed the product with id {productId} from the system.");
                isChanged = true;
            }
            else
                PrintErrorMessage($"The product id {productId} does not exist.");
        }
        private static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void PrintSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}