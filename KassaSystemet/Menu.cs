
namespace KassaSystemet
{
    public class Menu
    {
        public static void MainMenu()
        {
            var products = ProductCatalogue.Instance.Products;
            do
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
                            CustomerMenu(products);
                            menuOption = 0;
                            break;
                        case 2:
                            AdminMenu(products);
                            menuOption = 0;
                            break;
                        case 0:
                            FileManager.CreateFolders();
                            FileManager.SaveProductList(products);
                            App.CloseApp(products);
                            break;
                    }
                }
            } while (true);
        }
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
                        break;
                    case "2":
                        AddProduct(shoppingCart);
                        break;
                    case "3":
                        ProductCatalogue.Instance.DisplayProducts();
                        break;
                    case "0":
                        MainMenu();
                        break;
                    case "PAY":
                        Purchase.Pay(shoppingCart, products);
                        break;
                }
                Console.Write("Press any key to continue: ");
                Console.ReadKey();
            } while (true);
        }
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
                            productId = UserInputHandler.ProductIdInput();
                            ProductCatalogue.Instance.AddNewProduct();
                            break;
                        case 2:
                            Console.WriteLine("These are the available products in the system: ");
                            ProductCatalogue.Instance.DisplayProducts();
                            break;
                        case 3:
                            productId = UserInputHandler.ProductIdInput();
                            if (ProductCatalogue.IsProductAvailable(productId))
                                products[productId].ChangePrice();
                            else
                                Console.WriteLine($"The product id {productId} does not exist.");
                            break;
                        case 4:
                            productId = UserInputHandler.ProductIdInput();
                            if (ProductCatalogue.IsProductAvailable(productId))
                                ProductCatalogue.Instance.Products[productId].ChangeName();
                            else
                                Console.WriteLine($"The product id {productId} does not exist.");
                            break;
                        case 5:
                            Console.WriteLine("5. Add a discount for a product");
                            AddDiscount(products);
                            break;
                        case 6:
                            productId = UserInputHandler.ProductIdInput();
                            if (Discount.ContainsDiscount(products, productId))
                                Discount.Display(products[productId].Discounts);
                            else
                                Console.WriteLine($"The product id {productId} does not have a discount available.");
                            break;
                        case 7:
                            Product.DisplayAllDiscounts();
                            break;
                        case 8:
                            break;
                        case 9:
                            productId = UserInputHandler.ProductIdInput();
                            //RemoveProduct();
                            break;
                        case 0:
                            MainMenu();
                            break;
                    }
                    Console.Write("Press any key to continue: ");
                    Console.ReadKey();
                }
                else
                    Console.WriteLine("Incorrect input. Please enter a number 1-6.");
            } while (true);
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