
using System.Runtime.CompilerServices;

namespace KassaSystemet
{
    public class Menu
    {
        private static AdminMenuHandler adminMenuHandler;
        public static void MainMenu(FileManager fileManager)
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
                            CustomerMenu(fileManager);
                            break;
                        case "2":
                            AdminMenu(fileManager);
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
        private static void CustomerMenu(FileManager fileManager)
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
                        string receipt = Purchase.Pay(shoppingCart);
                        fileManager.SaveReceipt(receipt);
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
            } while (userInput != "0");
            MainMenu(fileManager);
        }
        private static void AdminMenu(FileManager fileManager)
        {
            adminMenuHandler ??= new AdminMenuHandler();

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
                bool isChanged = adminMenuHandler.HandleAdminMenuOption(userInput);
                if (isChanged)
                {
                    fileManager.SaveProductList();
                    fileManager.SaveDiscountList();
                }
                Console.ResetColor();
            } while (userInput != "0");
            MainMenu(fileManager);
        }
        private static void AddProduct(List<Purchase> shoppingCart)
        {
            (int id, decimal amount) = UserInputHandler.ProductInput();
            if (amount > 100)
                Console.WriteLine($"You can not purchase more than {100} of a product!", ConsoleColor.Red);
            else if (ProductCatalogue.Instance.Products.ContainsKey(id))
                shoppingCart.Add(new Purchase(id, amount));
            else
                Console.WriteLine($"No product with id {id} exist in the system.", ConsoleColor.Red);
        }
    }
}