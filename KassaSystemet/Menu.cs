﻿
namespace KassaSystemet
{
    public class Menu
    {
        public static void MainMenu() // Lägg till en loop så att den inte stänger ner
        {
            do
            {
                Console.Clear();
                Console.WriteLine("***Menu for the cash register***");
                Console.WriteLine("Choose an option below.");
                Console.WriteLine("1. New customer");
                Console.WriteLine("2. Admin tools");
                Console.WriteLine("0. Save & Exit.");
                Console.Write("Enter your command: ");
                string menuOption = Console.ReadLine();
                {
                    switch (menuOption)
                    {
                        case "1":
                            CustomerMenu();
                            break;
                        case "2":
                            AdminMenu();
                            break;
                        case "0":
                            App.CloseApp();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input.");
                            Thread.Sleep(1000);
                            Console.ResetColor();
                            break;
                    }
                }
            } while (true);
        }
        private static void CustomerMenu()
        {
            List<Purchase> shoppingCart = new();
            string userInput;
            do
            {
                Console.Clear();
                Console.WriteLine("****Welcome to the customer menu****");
                Console.WriteLine("1. Display your current cart.");
                Console.WriteLine("2. Enter products to purchase.");
                Console.WriteLine("3. Display available products.");
                Console.WriteLine("0: Clear shoppingcart and return to main menu.");
                Console.WriteLine("PAY: purchase wares in your cart and exit.");
                Console.Write("Enter command: ");
                userInput = Console.ReadLine().ToUpper();
                switch (userInput)
                {
                    case "1":
                        Purchase.DisplayPurchases(shoppingCart);
                        break;
                    case "2":
                        AddProduct(shoppingCart);
                        break;
                    case "3":
                        ProductCatalogue.Instance.DisplayProducts();
                        break;
                    case "0":
                        userInput = "0";
                        Console.WriteLine("Returning to the main menu.");
                        break;
                    case "PAY":
                        Purchase.Pay(shoppingCart);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input.");
                        Thread.Sleep(1000);
                        Console.ResetColor();
                        break;
                }
                Console.ResetColor();
                Console.Write("Press any key to continue: ");
                Console.ReadKey();
            } while (userInput != "0");
            MainMenu();
        }
        private static void AdminMenu()
        {
            string userInput;
            int productId;
            var productCatalogue = ProductCatalogue.Instance;
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
                {
                    switch (userInput)
                    {
                        case "1":
                            productId = UserInputHandler.ProductIdInput();
                            if (!productCatalogue.DoesProductExist(productId))
                                productCatalogue.AddNewProduct(productId);
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"The product id {productId} already exist.");
                            }
                            break;
                        case "2":
                            Console.WriteLine("These are the available products in the system: ");
                            productCatalogue.DisplayProducts();
                            break;
                        case "3":
                            productId = UserInputHandler.ProductIdInput();
                            if (productCatalogue.IsProductAvailable(productId))
                                productCatalogue.Products[productId].ChangeProductPrice();
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"The product id {productId} does not exist.");
                            }
                            break;
                        case "4":
                            productId = UserInputHandler.ProductIdInput();
                            if (productCatalogue.IsProductAvailable(productId))
                                productCatalogue.Products[productId].ChangeProductName();
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"The product id {productId} does not exist.");
                            }
                            break;
                        case "5":
                            Console.WriteLine("5. Add a discount for a product");
                            productId = UserInputHandler.ProductIdInput();
                            if (productCatalogue.IsProductAvailable(productId))
                                productCatalogue.AddNewDiscount(productId);
                            break;
                        case "6":
                            productId = UserInputHandler.ProductIdInput();
                            if (productCatalogue.ContainsDiscount(productId))
                                productCatalogue.Products[productId].Display();
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"The product id {productId} does not have a discount available.");
                            }
                            break;
                        case "7":
                            ProductCatalogue.DisplayAllDiscounts();
                            break;
                        case "8":
                            break;
                        case "9":
                            productId = UserInputHandler.ProductIdInput();
                            if (productCatalogue.IsProductAvailable(productId))
                            {
                                productCatalogue.RemoveProduct(productId);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Removed the product with id {productId} from the system.");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"The product id {productId} does not exist.");
                            }
                            break;
                        case "0":
                            userInput = "0";
                            Console.WriteLine("Return to the main menu.");
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input.");
                            Thread.Sleep(1000);
                            Console.ResetColor();
                            break;
                    }
                    Console.ResetColor();
                    Console.Write("Press any key to continue: ");
                    Console.ReadKey();
                }
            } while (userInput != "0");
            MainMenu();
        }
        private static void AddProduct(List<Purchase> shoppingCart)
        {
            (int id, decimal amount) = UserInputHandler.ProductInput();
            if (amount > 100)
                Console.WriteLine($"You can not purchase more than {100} of a product!");
            else if (ProductCatalogue.Instance.IsProductAvailable(id))
            {
                shoppingCart.Add(new Purchase(id, amount));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Added the product with id {id} to the shopping cart.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No product with id {id} exist in the system.");
            }
        }
    }
}