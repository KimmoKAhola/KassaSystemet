using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace KassaSystemet
{
    static internal class Menu
    {
        //static Product newProduct = new Product();
        public static Dictionary<int, Product> productDictionary = new Dictionary<int, Product>();
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
                    case 0:
                        menuOption = 0;
                        break;
                }
            } while (menuOption != 0);
        }

        public static void CustomerMenu()
        {
            Console.Clear();
            Console.WriteLine("Customer menu");
            Console.WriteLine("Commands:\n");
            Console.WriteLine("<Product ID> <Amount>");
            Console.WriteLine("PAY (exit and print receipt)");
            Console.Write("Enter command: ");
            string userInput = Console.ReadLine();
            do
            {
                switch (userInput)
                {
                    case "PAY":
                        Console.WriteLine("Create receipt and exit back to main menu");
                        MainMenu();
                        break;

                }
            } while (userInput != "PAY");

            /*
             * kund ska ange produktens ID samt antal/mängd
             * programmet ska fortsätta tills kund anger kommandot "PAY"
             * kvitto ska skrivas ut och sparas
             * */
        }

        public static void AdminMenu()
        {
            Console.Clear();
            Console.WriteLine("Admin menu");
            Console.WriteLine("1. Add\n2. Display products\n3. Change price on a product\n0. Exit");
            Console.Write("Enter command: ");
            string userInput = Console.ReadLine().ToUpper();
            do
            {
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
                        Console.WriteLine($"The product {name} with product ID {id} and unit price {price} has been added.");
                        Console.Write("Enter a new command: ");
                        userInput = Console.ReadLine();
                        break;

                    case "2":
                        Console.WriteLine("The dictionary contains these products: ");
                        Product.DisplayProducts(productDictionary);
                        Console.Write("Enter a new command: ");
                        userInput = Console.ReadLine();
                        break;

                    case "3":
                        Console.Write("Enter a product ID: ");
                        id = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter a new price: ");
                        price = Convert.ToDecimal(Console.ReadLine());
                        Product.ChangeProductPrice(productDictionary, id, price);
                        Console.Write("Enter a new command: ");
                        userInput = Console.ReadLine();
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
