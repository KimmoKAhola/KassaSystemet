
namespace KassaSystemet
{
    public class Menu
    {
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("***Menu for the cash register***");
            Console.WriteLine("Choose an option below.");
            Console.WriteLine("1. New customer");
            Console.WriteLine("2. Admin tools");
            Console.WriteLine("0. Save & Exit.");
            Console.Write("Enter your command: ");
            if (int.TryParse(Console.ReadLine(), out int menuOption))
            {
                switch (menuOption)
                {
                    case 1:
                        CustomerMenu();
                        break;
                    case 2:
                        AdminMenu();
                        break;
                    case 0:
                        App.CloseApp();
                        break;
                }
            }
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
                }
                Console.Write("Press any key to continue: ");
                Console.ReadKey();
            } while (userInput != "0");
            MainMenu();
        }
        private static void AdminMenu()
        {
            int userInput, productId;
            var products = ProductCatalogue.Instance.Products;
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
                if (int.TryParse(Console.ReadLine(), out userInput) && (userInput >= 0 && userInput <= 9))
                {
                    switch (userInput)
                    {
                        case 1:
                            productId = UserInputHandler.ProductIdInput();
                            if (!productCatalogue.DoesProductExist(productId))
                                productCatalogue.AddNewProduct(productId);
                            else
                                Console.WriteLine($"The product id {productId} already exist.");
                            break;
                        case 2:
                            Console.WriteLine("These are the available products in the system: ");
                            productCatalogue.DisplayProducts();
                            break;
                        case 3:
                            productId = UserInputHandler.ProductIdInput();
                            if (productCatalogue.IsProductAvailable(productId))
                                products[productId].ChangeProductPrice();
                            else
                                Console.WriteLine($"The product id {productId} does not exist.");
                            break;
                        case 4:
                            productId = UserInputHandler.ProductIdInput();
                            if (productCatalogue.IsProductAvailable(productId))
                                productCatalogue.Products[productId].ChangeProductName();
                            else
                                Console.WriteLine($"The product id {productId} does not exist.");
                            break;
                        case 5:
                            Console.WriteLine("5. Add a discount for a product");
                            productCatalogue.AddNewDiscount();
                            break;
                        case 6:
                            productId = UserInputHandler.ProductIdInput();
                            if (productCatalogue.ContainsDiscount(productId))
                                products[productId].Display();
                            else
                                Console.WriteLine($"The product id {productId} does not have a discount available.");
                            break;
                        case 7:
                            ProductCatalogue.DisplayAllDiscounts();
                            break;
                        case 8:
                            break;
                        case 9:
                            productId = UserInputHandler.ProductIdInput();
                            if (productCatalogue.IsProductAvailable(productId))
                                productCatalogue.RemoveProduct(productId);
                            else
                                Console.WriteLine($"The product id {productId} does not exist.");
                            break;
                        case 0:
                            userInput = 0;
                            Console.WriteLine("Return to the main menu.");
                            break;
                    }
                    Console.Write("Press any key to continue: ");
                    Console.ReadKey();
                }
                else
                    Console.WriteLine("Incorrect input. Please enter a number 1-6.");
            } while (userInput != 0);
            MainMenu();
        }
        private static void AddProduct(List<Purchase> shoppingCart)
        {
            (int id, decimal amount) = UserInputHandler.ProductInput();
            if (amount > 100)
                Console.WriteLine($"You can not purchase more than {100} of a product!");
            else if (ProductCatalogue.Instance.IsProductAvailable(id))
                shoppingCart.Add(new Purchase(id, amount));
            else
                Console.WriteLine($"No product with id {id} exist in the system.");
        }
    }
}