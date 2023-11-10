﻿using KassaSystemet.Interfaces;
using KassaSystemet.MenuPageServices;
using KassaSystemet.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.MenuPages
{
    public class AdminMenu : IMenu
    {
        private static AdminMenuHandler adminMenuHandler;
        private FileManager fileManager;
        public AdminMenu()
        {
            IFileManagerStrategy strategy = new FileManagerStrategy();
            fileManager ??= new FileManager(strategy);
            adminMenuHandler ??= new AdminMenuHandler();
        }

        public void DisplayMenu()
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
                Console.Write("Press any key to continue: ");
                Console.ReadKey();

            } while (userInput != "0");
        }
    }
}