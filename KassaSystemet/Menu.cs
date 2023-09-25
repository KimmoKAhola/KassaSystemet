using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KassaSystemet
{
    public class Menu
    {
        public void MainMenu()
        {
            int menuOption;

            do
            {
                Console.Clear();
                Console.WriteLine("***Menu for the cash register***");
                Console.WriteLine("Choose an option below.");
                Console.WriteLine("1. New customer");
                Console.WriteLine("2. Admin tools");
                Console.WriteLine("3. Load receipt ID file ** TEST ONLY DELETE LATER**");
                Console.WriteLine("4. Press 4 for a demonstration with hardcoded values");
                Console.WriteLine("0. Avsluta. This saves all dictionaries to files");
                Console.Write("Enter your command: ");
                if (int.TryParse(Console.ReadLine(), out menuOption))
                {
                    switch (menuOption)
                    {
                        case 1:
                            //CustomerMenu customerMenu = new();
                            CustomerMenu();
                            menuOption = 0;
                            break;
                        case 2:
                            AdminMenu();
                            menuOption = 0;
                            break;
                        case 3:
                            Console.WriteLine("receipt id value from file: " + FileManager.GetReceiptID());
                            Console.ReadKey();
                            break;
                        case 0:
                            FileManager.SaveProductList(Product.productDictionary);
                            FileManager.SaveDiscountList(Discount.allDiscounts);
                            Environment.Exit(0);
                            break;
                    }
                }
            } while (menuOption != 0);
        }

        private void CustomerMenu()
        {

            /*
             * kund ska ange produktens ID samt antal/mängd
             * programmet ska fortsätta tills kund anger kommandot "PAY"
             * kvitto ska skrivas ut och sparas
             */
            string userInput;
            do
            {
                Console.Clear();
                Console.WriteLine("****Welcome to the customer menu****");
                Console.WriteLine("1. Display your current cart.");
                Console.WriteLine("2. Enter products to purchase.");
                Console.WriteLine("3. Display available products.");
                Console.WriteLine("PAY: purchase wares in your cart and exit.");
                Console.Write("Enter command: ");
                userInput = Console.ReadLine().ToUpper();
                switch (userInput)
                {
                    case "1":
                        Purchase.DisplayShoppingCart(Purchase.shoppingCart);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case "2":
                        PurchaseProducts();
                        Console.WriteLine("Add another product? (press 2) does not work atm");
                        userInput = Console.ReadLine();
                        break;

                    case "3":
                        Product.DisplayProducts(Product.productDictionary);
                        Console.ReadKey();
                        break;

                    case "PAY":
                        Console.WriteLine("Purchase the wares in your shopping cart. This saves the receipt to a file.");
                        Purchase.Pay(); // pay command in purchase class
                        userInput = "0";
                        break;

                    case "0":
                        MainMenu();
                        break;
                }
            } while (userInput != "0");
            MainMenu();
        }
        private void AdminMenu()
        {

            int userInput;
            do
            {
                Console.Clear();
                Console.WriteLine("Admin menu");
                Console.WriteLine("1. Add a new product to the system\n" +
                    "2. Display available products in the system\n" +
                    "3. Change price on a product\n" +
                    "4. Change name on a product\n" +
                    "5. Add a discount for a product\n" +
                    "6. Display all available discounts\n" +
                    "7. Remove a discount from a product.\n" +
                    "0. Exit admin menu");

                Console.Write("Enter a command: ");
                //userInput = Console.ReadLine().ToUpper();
                if (int.TryParse(Console.ReadLine(), out userInput) && (userInput >= 0 && userInput < 8))
                {
                    switch (userInput)
                    {
                        case 1:
                            //Add a new product to the system.
                            AddNewProduct();
                            Console.ReadKey();
                            break;

                        case 2:
                            //Display the products
                            Console.Write("These are the available products in the system: ");
                            Product.DisplayProducts(Product.productDictionary);
                            Console.Write("Press any key to continue. ");
                            Console.ReadKey();
                            break;

                        case 3:
                            ChangePriceOnProduct();
                            Console.Write("Press any key to continue. ");
                            Console.ReadKey();
                            break;

                        case 4:
                            ChangeNameOnProduct();
                            Console.Write("Press any key to continue. ");
                            Console.ReadKey();
                            break;

                        case 5:
                            AddNewDiscount();
                            Console.Write("Press any key to continue");
                            break;
                        case 6:
                            Discount.DisplayAllDiscounts(Discount.allDiscounts);
                            Console.ReadKey();
                            Console.Write("Press any key to continue");
                            break;
                        case 7:
                            RemoveDiscount();
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            break;

                        case 0:
                            MainMenu();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect input. Please enter a number 1-6.");
                }
            } while (userInput != 0);
            MainMenu();
        }

        private static void PurchaseProducts()
        {
            Console.WriteLine("Enter wares to your purchase, then print the receipt");
            Console.Write("Enter <product ID> <Amount>: ");
            string customerEntry = Console.ReadLine();
            string[] entries = customerEntry.Split(' ');
            if (entries.Length != 2)
            {
                Console.WriteLine("You entered the information incorrectly. Try again!");
            }
            int productID = Convert.ToInt32(entries[0]);
            decimal amount = Convert.ToDecimal(entries[1]);
            Purchase.shoppingCart.Add(new Purchase(productID, amount));
        }

        private static void AddNewProduct()
        {
            Console.Write("Adding a new product. Enter id, name and price: ");
            string adminEntry = Console.ReadLine();
            string[] adminEntries = adminEntry.Split(' ');
            if (adminEntries.Length != 3)
            {
                Console.WriteLine("You entered the values incorrectly. Try again!");
            }
            else
            {

                int id = Convert.ToInt32(adminEntries[0]);
                string name = adminEntries[1];
                decimal price = Convert.ToDecimal(adminEntries[2]);
                string priceType = "per unit"; // Change later. Hard coded for now

                Product.AddNewProduct(Product.productDictionary, id, name, price, priceType);
            }
        }
        private static void ChangePriceOnProduct()
        {
            //Change price on a product
            Console.Write("Enter a product ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            if (Product.DoesProductExist(id))
            {
                Console.WriteLine($"The current price for your product with id [{id}] is: {Product.FindProductPrice(Product.productDictionary, id)}");
                Console.Write("Enter a new price: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());
                Product.ChangeProductPrice(Product.productDictionary, id, price);
            }
        }
        private static void ChangeNameOnProduct()
        {
            Console.Write("Enter the name of the product you want to change: ");
            string oldName = Console.ReadLine();
            Console.Write("Enter the new name: ");
            string newName = Console.ReadLine();
            Product.ChangeProductName(Product.productDictionary, oldName, newName);
        }

        private static void AddNewDiscount()
        {
            Console.Write("Enter a product ID (Bananer=300): ");
            int inputID = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter a start date: (YYYY/MM/DD): ");
            string startDate = Console.ReadLine();
            Console.Write("Enter a start date (YYYY/MM/DD): ");
            string endDate = Console.ReadLine();
            Console.Write("Enter a discount percentage (ex. 70 %): ");
            decimal discountPercentage = Convert.ToDecimal(Console.ReadLine());
            Discount.AddNewDiscount(inputID, startDate, endDate, discountPercentage);
        }
        private static void RemoveDiscount()
        {
            Console.Write("Enter a product ID: ");
            int inputID = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter a start date: (YYYY-MM-DD): ");
            string startDate = Console.ReadLine();
            Console.Write("Enter an end date: (YYYY-MM-DD): ");
            string endDate = Console.ReadLine();

            Discount.RemoveDiscount(Discount.allDiscounts, inputID, startDate, endDate);
        }

    }
}
