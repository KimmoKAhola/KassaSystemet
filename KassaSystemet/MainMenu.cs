using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KassaSystemet
{
    static internal class Menu
    {
        public static Dictionary<int, Product> productDictionary = new();
        public static List<Purchase> listOfCurrentWares = new(); // Lägg in varor här. Vid köp, spara till kvitto och rensa sedan
        public static List<Product> productList = new();
        static int receiptCounter = Receipt.GetReceiptID();

        public static void MainMenu()
        {
            int menuOption = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("***Menu for the cash register***");
                Console.WriteLine("Choose an option below.");
                Console.WriteLine("1. Ny Kund");
                Console.WriteLine("2. Admin");
                Console.WriteLine("3. Load receipt ID file ** TEST ONLY DELETE LATER**");
                Console.WriteLine("0. Avsluta");
                Console.Write("Enter your command: ");
                menuOption = Convert.ToInt32(Console.ReadLine());

                switch (menuOption)
                {
                    case 1:
                        CustomerMenu();
                        menuOption = 0;
                        break;
                    case 2:
                        AdminMenu();
                        menuOption = 0;
                        break;
                    case 3:
                        Receipt.GetReceiptID();
                        Console.ReadKey();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                }
            } while (menuOption != 0);
        }

        public static void CustomerMenu()
        {
            /*
             * kund ska ange produktens ID samt antal/mängd
             * programmet ska fortsätta tills kund anger kommandot "PAY"
             * kvitto ska skrivas ut och sparas
             * 
             */

            Console.Clear();
            Console.WriteLine("Customer menu");
            Console.WriteLine("Commands:\n");
            Console.WriteLine("<Product ID> <Amount>");
            Console.WriteLine("PAY (exit and print receipt)");
            Console.Write("Enter command: ");
            string userInput = Console.ReadLine().ToUpper();
            do
            {
                switch (userInput)
                {
                    case "PAY":
                        Console.WriteLine("Create receipt and exit back to main menu");
                        int receiptID = ++receiptCounter;
                        productList.Add(new Product("bananas", 1, 19.50m));
                        Receipt.CreateReceipt(productList, receiptID);
                        Receipt.CreateReceiptIDFile(receiptID);
                        CustomerMenu();
                        break;
                    case "0":
                        MainMenu();
                        break;
                }
            } while (userInput != "0");
            MainMenu();
        }

        public static void AdminMenu()
        {
            string userInput;
            do
            {
                Console.Clear();
                Console.WriteLine("Admin menu");
                Console.WriteLine("1. Add a new product\n" +
                    "2. Display available products\n" +
                    "3. Change price on a product\n" +
                    "4. Save your current product list to a file\n" +
                    "0. Exit admin menu");

                Console.Write("Enter a command: ");
                userInput = Console.ReadLine().ToUpper();
                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("Add a new product");
                        Console.Write("Enter product name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter product ID: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter price per unit: ");
                        decimal price = Convert.ToDecimal(Console.ReadLine());
                        Product newProduct = new Product(name, id, price);
                        Product.AddNewProduct(productDictionary, newProduct);
                        Console.WriteLine($"The product {name} with product ID {id} and unit price {price} has been added.\n" +
                            $"Press any key to continue.");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Write("The dictionary contains these products: ");
                        Product.DisplayProducts(productDictionary);
                        Console.Write("Press any key to continue. ");
                        Console.ReadKey();
                        break;

                    case "3":
                        Console.Write("Enter a product ID: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter a new price: ");
                        price = Convert.ToDecimal(Console.ReadLine());
                        Product.ChangeProductPrice(productDictionary, id, price);
                        Console.Write("Press any key to continue. ");
                        Console.ReadKey();
                        break;

                    case "4":
                        Product.SaveToFile(productDictionary);
                        Console.WriteLine("The list of products has been saved to a file. ");
                        Console.Write("Press any key to continue. ");
                        Console.ReadKey();
                        break;

                    case "0":
                        MainMenu();
                        break;
                }
            } while (userInput != "0");
            MainMenu();
        }
    }
}
