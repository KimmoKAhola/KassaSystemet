﻿using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Models;

namespace KassaSystemet.MenuPageServices
{
    public class CustomerMenuHandler
    {
        private List<Purchase> _shoppingCart;
        public CustomerMenuHandler()
        {
            _shoppingCart = new List<Purchase>();

        }

        public void HandleCustomerMenuOption(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    Purchase.DisplayPurchases(_shoppingCart);
                    break;
                case "2":
                    AddProduct(_shoppingCart);
                    break;
                case "3":
                    ProductCatalogue.Instance.DisplayProducts();
                    break;
                case "0":
                    userInput = "0";
                    Console.WriteLine("Returning to the main menu.");
                    break;
                case "PAY":
                    string receipt = Purchase.Pay(_shoppingCart);
                    //fileManager.SaveReceipt(receipt);
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
