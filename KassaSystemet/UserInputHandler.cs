using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public static class UserInputHandler
    {
        public static (int first, decimal second) ProductInput()
        {
            int id;
            decimal amount;
            while (true)
            {
                Console.ResetColor();
                Console.Write("Enter product id and an amount larger than 0: ");
                string[] userInput = Console.ReadLine().Split(' ');

                if (userInput.Length == 2 && int.TryParse(userInput[0], out id) && decimal.TryParse(userInput[1], out amount) && amount > 0)
                    return (id, amount);
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter an integer and a decimal value separated by a space.");
                }
            }
        }
        public static int ProductIdInput()
        {
            int userInput = 0;
            while (true)
            {
                Console.Write("Enter a product id: ");
                if (int.TryParse(Console.ReadLine(), out int number) && number <= 999 && number >= 100)
                {
                    userInput = number;
                    break;
                }
                else
                    Console.WriteLine("Incorrect input. Please enter a 3-digit product ID.");
            }
            return userInput;
        }
        public static (string startDate, string endDate, decimal discountPercentage) DiscountInput()
        {
            while (true)
            {
                Console.ResetColor();
                Console.Write("Enter a start date, end date and percentage (yyyy-MM-dd) (yyyy-MM-dd) (percentage) separated by a space: " +
                    "\nExample: 2023-09-10 2023-09-15 75: ");
                string[] userInput = Console.ReadLine().Split(' ');

                if (userInput.Length == 3 &&
                    DateOnly.TryParseExact(userInput[0], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly startDate) &&
                    DateOnly.TryParseExact(userInput[1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly endDate) &&
                    decimal.TryParse(userInput[2], out decimal discountPercentage))
                {
                    return (startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), discountPercentage);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter valid dates (yyyy-MM-dd) and a valid percentage value separated by spaces.");
                }
            }
        }
        public static (string productName, decimal price, string priceType) NewProduct()
        {
            string productName = "";
            decimal price;
            string priceType;

            while (productName.Length <= 1)
            {
                Console.Write("Enter a product name, at least 2 character long: ");
                productName = Console.ReadLine();
            }
            Console.Write("Enter a price: ");
            while (!decimal.TryParse(Console.ReadLine(), out price) || price <= 0)
            {
                Console.WriteLine("Price must be a positive decimal value. Please try again.");
                Console.Write("Enter a price: ");
            }

            while (true)
            {
                Console.Write("Enter a product price type (per kg/per unit): ");
                string userInput = Console.ReadLine().ToLower();

                if (userInput == "per kg" || userInput == "per unit")
                {
                    priceType = userInput;
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid price type. Please enter 'per kg' or 'per unit'.");
                }
            }
            return (productName, price, priceType);
        }
    }
}