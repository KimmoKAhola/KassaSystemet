
using System.Runtime.CompilerServices;

namespace KassaSystemet
{
    public class Menu
    {
        /// <summary>
        /// The main menu containing the navigation
        /// for the whole program.
        /// </summary>
        ///

        public static void MainMenu(Dictionary<int, Product> products)
        {
            int menuOption;
            do
            {
                Console.Clear();
                Console.WriteLine("***Menu for the cash register***");
                Console.WriteLine("Choose an option below.");
                Console.WriteLine("1. New customer");
                Console.WriteLine("2. Admin tools");
                Console.WriteLine("0. Save & Exit.");
                Console.Write("Enter your command: ");
                if (int.TryParse(Console.ReadLine(), out menuOption))
                {
                    switch (menuOption)
                    {
                        case 1:
                            CustomerMenu(products);
                            menuOption = 0;
                            break;
                        case 2:
                            AdminMenu(products);
                            menuOption = 0;
                            break;
                        case 3:
                            Console.WriteLine("receipt id value from file: " + FileManager.GetReceiptID());
                            Console.ReadKey();
                            break;
                        case 0:
                            FileManager.CreateFolders();
                            FileManager.SaveProductList(products);
                            App.CloseApp(products);
                            //TODO Save here
                            Environment.Exit(0);
                            break;
                    }
                }
            } while (menuOption != 0);
        }
        /// <summary>
        /// The sub menu containing the navigation
        /// for the customer options.
        /// </summary>
        public static void CustomerMenu(Dictionary<int, Product> products)
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
                Console.WriteLine("0: Return to main menu.");
                Console.WriteLine("PAY: purchase wares in your cart and exit.");
                Console.Write("Enter command: ");
                userInput = Console.ReadLine().ToUpper();
                switch (userInput)
                {
                    case "1":
                        Purchase.DisplayPurchases(shoppingCart, products);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case "2":
                        AddProduct(shoppingCart);
                        Console.WriteLine("Add another product? (press 2) does not work atm");
                        userInput = Console.ReadLine();
                        break;
                    case "3":
                        ProductDataBase.DisplayProducts(products);
                        Console.ReadKey();
                        break;
                    case "0":
                        userInput = "0";
                        break;
                    case "PAY":
                        Console.WriteLine("Purchase the wares in your shopping cart. This saves the receipt to a file.");
                        Purchase.Pay(shoppingCart, products);
                        userInput = "0";
                        break;
                }
            } while (userInput != "0");
            MainMenu(products);
        }
        /// <summary>
        /// The sub menu containing the navigation
        /// for the admin options.
        /// </summary>
        private static void AdminMenu(Dictionary<int, Product> products)
        {
            int userInput, productId;
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
                if (int.TryParse(Console.ReadLine(), out userInput) && (userInput >= 0 && userInput <= 9))
                {
                    switch (userInput)
                    {
                        case 1:
                            //Add a new product to the system.
                            productId = UserInputHandler.ProductIdInput();
                            Product.AddNewProduct(products, productId);
                            Console.ReadKey();
                            break;

                        case 2:
                            Console.WriteLine("These are the available products in the system: ");
                            Product.DisplayProducts(products);
                            Console.Write("Press any key to continue. ");
                            Console.ReadKey();
                            break;

                        case 3:
                            productId = UserInputHandler.ProductIdInput();
                            Product.ChangePrice(products, productId);
                            Console.Write("Press any key to continue. ");
                            Console.ReadKey();
                            break;

                        case 4:
                            productId = UserInputHandler.ProductIdInput();
                            Product.ChangeName(products, productId);
                            Console.Write("Press any key to continue. ");
                            Console.ReadKey();
                            break;

                        case 5:
                            Console.WriteLine("5. Add a discount for a product");
                            AddDiscount(products);
                            Console.Write("Press any key to continue. ");
                            Console.ReadKey();
                            break;
                        case 6:
                            productId = UserInputHandler.ProductIdInput();
                            if (Discount.ContainsDiscount(products, productId))
                                Discount.Display(products[productId].Discounts);
                            else
                                Console.WriteLine($"The product id {productId} does not have a discount available.");
                            Console.ReadKey();
                            Console.Write("Press any key to continue");
                            break;
                        case 7:
                            Product.DisplayAllDiscounts(products);
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            break;
                        case 8:
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            break;
                        case 9:
                            productId = UserInputHandler.ProductIdInput();
                            Product.RemoveProduct(products, productId);
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input. Please enter a number 1-6.");
                }
            } while (userInput != 0);
            MainMenu(products);
        }
        public static void AddProduct(List<Purchase> shoppingCart)
        {
            (int id, decimal amount) = UserInputHandler.ProductInput();
            shoppingCart.Add(new Purchase(id, amount));
        }

        public static void AddDiscount(Dictionary<int, Product> products)
        {
            (int productId, string startDate, string endDate, decimal discountPercentage) = UserInputHandler.DiscountInput();
            products[productId].AddDiscount(new Discount(startDate, endDate, discountPercentage));
        }
    }
}
