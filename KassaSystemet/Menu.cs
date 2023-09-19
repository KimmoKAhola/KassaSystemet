﻿using System;
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
    public static class Menu
    {
        public static Dictionary<int, Product> seedDictionary = Seed.seedDictionary;

        public static List<Purchase> seedCart = Seed.seedProductList;
        public static List<Purchase> shoppingCart = new List<Purchase>(); //Empty shopping cart
        public static void MainMenu()
        {
            //Product.FindProductPrice(testDictionary, 300);
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
                        FileManager.GetReceiptID();
                        Console.ReadKey();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                }
            } while (menuOption != 0);
        }

        private static void CustomerMenu()
        {
            /*
             * kund ska ange produktens ID samt antal/mängd
             * programmet ska fortsätta tills kund anger kommandot "PAY"
             * kvitto ska skrivas ut och sparas
             * 
             */

            string userInput;
            do
            {
                Console.Clear();
                Console.WriteLine("****Welcome to the customer menu****");
                Console.WriteLine("1. Display your current cart");
                Console.WriteLine("2. Enter products to purchase");
                Console.WriteLine("00. Empty");
                Console.WriteLine("PAY: purchase wares in your cart and exit.");
                Console.Write("Enter command: ");
                userInput = Console.ReadLine().ToUpper();
                switch (userInput)
                {
                    case "1":
                        Purchase.DisplayShoppingCart(seedCart);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case "2":
                        CustomerCase2();
                        Console.WriteLine("Add another product? (press 2) does not work atm");
                        userInput = Console.ReadLine();
                        break;
                    case "PAY":
                        //FileManager.CreateReceiptIDFile(receiptID);
                        Console.WriteLine("Purchase the wares in your shopping cart. This saves the receipt to a file.");
                        Purchase.Pay(); // pay command in purchase class
                        userInput = "0";
                        break;
                    case "00":
                        Console.WriteLine("Nothing here");
                        Console.ReadKey();
                        break;
                    case "0":
                        MainMenu();
                        break;
                }
            } while (userInput != "0");
            MainMenu();
        }

        private static void AdminMenu()
        {
            string userInput;
            do
            {
                Console.Clear();
                Console.WriteLine("Admin menu");
                Console.WriteLine("1. Add a new product to the system\n" +
                    "2. Display available products in the system\n" +
                    "3. Change price on a product\n" +
                    "4. Change name on a product\n" +
                    "0. Exit admin menu");

                Console.Write("Enter a command: ");
                userInput = Console.ReadLine().ToUpper();
                switch (userInput)
                {
                    case "1":
                        AddNewProduct();
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Write("These are the available products in the system: ");
                        Product.DisplayProducts(seedDictionary);
                        Console.Write("Press any key to continue. ");
                        Console.ReadKey();
                        break;

                    case "3":
                        Case3();
                        Console.Write("Press any key to continue. ");
                        Console.ReadKey();
                        break;

                    case "4":
                        Case4();
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

        private static (string name, decimal price) EnterNamePrice()
        {
            Console.Write("Enter a name and a price: ");
            string entry = Console.ReadLine();
            string[] entries = entry.Split(' ');
            
            return (entries[0], Convert.ToDecimal(entries[1]));
        }
        private static void CustomerCase2()
        {
            Console.WriteLine("Enter wares to your purchase, then print the receipt");
            Console.Write("Enter <product ID> <Amount>: ");
            string customerEntry = Console.ReadLine();
            string[] entries = customerEntry.Split(' ');
            int amount = Convert.ToInt32(entries[1]);
            seedCart.Add(new Purchase(entries[0], amount)); // check this!
            Console.WriteLine($"Added {entries[0]} and {amount} to your cart!");
        }

        private static void AddNewProduct()
        {
            Console.Write("Adding a new product. Enter id, name and price: ");
            string adminEntry = Console.ReadLine();
            string[] adminEntries = adminEntry.Split(' ');

            int id = Convert.ToInt32(adminEntries[0]);
            string name = adminEntries[1];
            decimal price = Convert.ToDecimal(adminEntries[2]);

            Product.AddNewProduct(seedDictionary, id, name, price);
        }
        private static void Case4()
        {
            Console.Write("Enter the name of the product you want to change: ");
            string oldName = Console.ReadLine();
            Console.Write("Enter the new name: ");
            string newName = Console.ReadLine();
            Product.ChangeProductName(seedDictionary, oldName, newName);
        }
        private static void Case3()
        {
            Console.Write("Enter a product ID: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"The current price for your product with id [{id}] is: {Product.FindProductPrice(seedDictionary, id)}");
            Console.Write("Enter a new price: ");
            decimal price = Convert.ToDecimal(Console.ReadLine());
            Product.ChangeProductPrice(seedDictionary, id, price);
        }
    }
}
