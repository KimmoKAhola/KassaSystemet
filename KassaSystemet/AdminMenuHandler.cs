using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public static class AdminMenuHandler
    {
        private static ProductCatalogue productCatalogue = ProductCatalogue.Instance;
        public static void HandleAdminMenuOption(string userInput, FileManager fileManager)
        {
            switch (userInput)
            {
                case "1":
                    AddNewProduct(fileManager);
                    break;
                case "2":
                    DisplayAvailableProducts();
                    break;
                case "3":
                    ChangeProductPrice();
                    break;
                case "4":
                    ChangeProductName();
                    break;
                case "5":
                    AddProductDiscount(fileManager);
                    break;
                case "6":
                    DisplayProductDiscount();
                    break;
                case "7":
                    DisplayAllDiscounts();
                    break;
                case "8":
                    RemoveProductDiscount(fileManager);
                    break;
                case "9":
                    RemoveProduct();
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
        }
        private static void AddNewProduct(FileManager fileManager)
        {
            int productId = UserInputHandler.ProductIdInput();
            if (!productCatalogue.Products.ContainsKey(productId))
            {
                productCatalogue.AddNewProduct(productId);
                fileManager.SaveProductList();
            }
            else
                Console.WriteLine($"The product id {productId} already exists.", Console.ForegroundColor = ConsoleColor.Red);
        }
        private static void DisplayAvailableProducts()
        {
            Console.WriteLine("These are the available products in the system: ");
            productCatalogue.DisplayProducts();
        }
        private static void ChangeProductPrice()
        {
            int productId = UserInputHandler.ProductIdInput();
            if (productCatalogue.Products.ContainsKey(productId))
                productCatalogue.Products[productId].ChangeProductPrice();
            else
                Console.WriteLine($"The product id {productId} does not exist.", Console.ForegroundColor = ConsoleColor.Red);
        }
        private static void ChangeProductName()
        {
            int productId = UserInputHandler.ProductIdInput();
            if (productCatalogue.Products.ContainsKey(productId))
                productCatalogue.Products[productId].ChangeProductName();
            else
            {
                Console.WriteLine($"The product id {productId} does not exist.", Console.ForegroundColor = ConsoleColor.Red);
            }
        }
        private static void AddProductDiscount(FileManager fileManager)
        {
            Console.WriteLine("5. Add a discount for a product");
            int productId = UserInputHandler.ProductIdInput();

            if (productCatalogue.Products.ContainsKey(productId))
            {
                productCatalogue.AddNewDiscount(productId);
                fileManager.SaveDiscountList();
            }
            else
            {
                Console.WriteLine($"The product id {productId} does not exist.", Console.ForegroundColor = ConsoleColor.Red);
            }
        }
        private static void DisplayProductDiscount()
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
        private static void RemoveProductDiscount(FileManager fileManager)
        {
            int productId = UserInputHandler.ProductIdInput();
            if (productCatalogue.ContainsDiscount(productId))
            {
                productCatalogue.Products[productId].RemoveDiscount();
                fileManager.SaveDiscountList();
            }
            else
            {
                Console.WriteLine($"The product id {productId} does not have a discount available.", Console.ForegroundColor = ConsoleColor.Red);
            }
        }
        private static void RemoveProduct()
        {
            int productId = UserInputHandler.ProductIdInput();
            if (productCatalogue.Products.ContainsKey(productId))
            {
                productCatalogue.RemoveProduct(productId);
                Console.WriteLine($"Removed the product with id {productId} from the system.", Console.ForegroundColor = ConsoleColor.Green);
            }
            else
                Console.WriteLine($"The product id {productId} does not exist.", Console.ForegroundColor = ConsoleColor.Red);
        }
    }
}
