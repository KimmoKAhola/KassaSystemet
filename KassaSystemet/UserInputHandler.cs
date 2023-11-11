using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public class UserInputHandler : IUserInputHandler
    {
        public (int, decimal) ProductInput()
        {
            while (true)
            {
                Console.Write("Enter product id and a product amount larger than 0: ");
                string[] userInput = Console.ReadLine().Split(' ');

                if (IsValidInput(userInput, out int id, out decimal amount))
                    return (id, amount);
                else
                    PrintErrorMessage();
            }
        }

        private static bool IsValidInput(string[] userInput, out int productId, out decimal productAmount)
        {
            productId = 0;
            productAmount = 0;

            return (userInput.Length == 2 && int.TryParse(userInput[0], out productId) && decimal.TryParse(userInput[1], out productAmount) && productAmount > 0);
        }

        public int ProductIdInput()
        {
            while (true)
            {
                Console.Write("Enter a 3-digit product id: ");
                string userInput = Console.ReadLine();

                if (IsValidInput(userInput))
                    return Convert.ToInt32(userInput);
                else
                    PrintErrorMessage();
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
                Console.Write("Enter a start date, end date and percentage (yyyy-MM-dd) (yyyy-MM-dd) (percentage) separated by a space: " +
                    "\nExample: 2023-09-10 2023-09-15 75: ");
                string[] userInput = Console.ReadLine().Split(' ');

                if (IsValidInput(userInput, out DateOnly startDate, out DateOnly endDate, out decimal discountPercentage))
                    return (startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), discountPercentage);
                else
                    PrintErrorMessage();
            }
        }

        private static bool IsValidInput(string[] userInput, out DateOnly startDate, out DateOnly endDate, out decimal discountPercentage)
        {
            startDate = default;
            endDate = default;
            discountPercentage = 0m;

            return (userInput.Length == 3 &&
                    DateOnly.TryParseExact(userInput[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate) &&
                    DateOnly.TryParseExact(userInput[1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate) &&
                    decimal.TryParse(userInput[2], out discountPercentage));
        }
        public (string, decimal, string) NewProduct()
        {
            string productName = GetValidProductName();
            decimal price = GetValidProductPrice();
            string priceType = GetValidProductPriceType();

            return (productName, price, priceType);
        }
        private static string GetValidProductName()
        {
            string productName = "";
            while (productName.Length <= 1)
            {
                Console.Write("Enter a product name, at least 2 character long: ");
                productName = Console.ReadLine();
            }
            return productName;
        }

        private static decimal GetValidProductPrice()
        {
            decimal price;
            Console.Write($"Enter a price above {0:C2}: ");
            while (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
            {
                PrintErrorMessage();
            }
            return price;
        }

        private static string GetValidProductPriceType()
        {
            while (true)
            {
                Console.Write("Enter a product price type (per kg/per unit): ");
                string userInput = Console.ReadLine().ToLower();

                if (userInput == "per kg" || userInput == "per unit")
                    return userInput;
                else
                    Console.WriteLine("Invalid price type. Please enter 'per kg' or 'per unit'.");
            }
        }
        private static void PrintErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please enter your input as prompted.");
            Console.ResetColor();
        }
    }
}