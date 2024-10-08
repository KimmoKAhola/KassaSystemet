﻿using KassaSystemet.Interfaces;
using KassaSystemet.Menus.MenuPageHandlers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using KassaSystemet.Menus.MenuPages;

namespace KassaSystemet.Utilities
{
    public class UserInputHandler : IUserInputHandler
    {
        public (int, decimal) ProductInput()
        {
            while (true)
            {
                PromptUser("Enter product id and a product amount larger than 0: ");
                string[] userInput = Console.ReadLine().Split(' ');

                if (IsValidInput(userInput, out int id, out decimal amount))
                    return (id, amount);
                else
                    PrintErrorMessage("Invalid input!");
            }
        }
        private static bool IsValidInput(string[] userInput, out int productId, out decimal productAmount)
        {
            productId = 0;
            productAmount = 0;

            return userInput.Length == 2 && int.TryParse(userInput[0], out productId) && decimal.TryParse(userInput[1], out productAmount) && productAmount > 0;
        }
        public int ProductIdInput()
        {
            while (true)
            {
                PromptUser("Enter a 3-digit product id: ");
                string userInput = Console.ReadLine();

                if (IsValidInput(userInput))
                    return Convert.ToInt32(userInput);
                else
                    PrintErrorMessage("Invalid input!");
            }
        }
        private static bool IsValidInput(string userInput)
        {
            return int.TryParse(userInput, out int productId) && productId <= 999 && productId >= 100;
        }
        public (string, string, decimal) DiscountInput()
        {
            while (true)
            {
                PromptUser("Enter a start date, end date and percentage (yyyy-MM-dd) (yyyy-MM-dd) (percentage 0-100) separated by a space: " +
                    "\nExample: 2023-09-10 2023-09-15 75: ");
                string[] userInput = Console.ReadLine().Split(' ');

                if (IsValidInput(userInput, out DateOnly startDate, out DateOnly endDate, out decimal discountPercentage) && discountPercentage > 0 && discountPercentage < 100)
                    return (startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), discountPercentage);
                else
                    PrintErrorMessage("Please enter the input as in the given example.");
            }
        }
        private static bool IsValidInput(string[] userInput, out DateOnly startDate, out DateOnly endDate, out decimal discountPercentage)
        {
            startDate = default;
            endDate = default;
            discountPercentage = 0m;

            return userInput.Length == 3 &&
                    DateOnly.TryParseExact(userInput[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate) &&
                    DateOnly.TryParseExact(userInput[1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate) &&
                    decimal.TryParse(userInput[2], out discountPercentage);
        }
        public (string, decimal, string) NewProduct()
        {
            string productName = GetValidProductName();
            decimal price = GetValidProductPrice();
            string priceType = GetValidProductPriceType();

            return (productName, price, priceType);
        }
        public string GetValidProductName()
        {
            string productName = "";
            while (productName.Length < 2 || !productName.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            {
                PromptUser("Enter a product name, at least 2 character long, without any numbers or special characters: ");
                productName = Console.ReadLine();
                productName = productName.TrimStart();
                productName = productName.TrimEnd();
            }
            return productName;
        }
        public decimal GetValidProductPrice()
        {
            decimal price;
            PromptUser($"Enter a price above {0:C2} and below {1E8 + 1:C2}: ");
            while (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
            {
                PrintErrorMessage($"Enter a price above {0:C2} and below {1E8 + 1:C2}: ");
            }
            return price;
        }
        private static string GetValidProductPriceType()
        {
            while (true)
            {
                PromptUser("Enter a product price type (per kg/per unit): ");
                string userInput = Console.ReadLine().ToLower();

                if (userInput == "per kg" || userInput == "per unit")
                    return userInput;
                else
                    PrintErrorMessage("Invalid price type. Please enter 'per kg' or 'per unit'.");
            }
        }
        public TEnum GetMenuEnum<TEnum>() where TEnum : struct
        {
            while (true)
            {
                PromptUser("Enter your command: ");
                string userInput = Console.ReadLine();
                if (IsValidEnumInput(userInput, out TEnum result))
                    return result;
                else
                    PrintErrorMessage("Enter a valid input");
            }
        }
        public bool IsValidEnumInput<TEnum>(string userInput, out TEnum result) where TEnum : struct
        {
            while (true)
            {
                return Enum.TryParse(userInput, out result);
            }
        }
        private static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void PromptUser(string message) => Console.Write(message);
    }
}