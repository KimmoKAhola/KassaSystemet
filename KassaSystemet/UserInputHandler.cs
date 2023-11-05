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
                Console.Write("Enter id and amount: ");
                string[] userInput = Console.ReadLine().Split(' ');

                if (userInput.Length == 2 && int.TryParse(userInput[0], out id) && decimal.TryParse(userInput[1], out amount))
                    return (id, amount);
                else
                    Console.WriteLine("Invalid input. Please enter an integer and a decimal separated by a space.");
            }
        }
        public static int ProductIdInput()
        {
            int userInput = 0;
            while (true)
            {
                Console.Write("Enter a product id: ");
                if (int.TryParse(Console.ReadLine(), out int number) && number < 999 && number > 100)
                {
                    userInput = number;
                    break;
                }
                else
                    Console.WriteLine("Incorrect input. Please enter a 3-digit product ID.");
            }
            return userInput;
        }
        public static (int productId, string startDate, string endDate, decimal discountPercentage) DiscountInput()
        {
            while (true)
            {
                Console.Write("Enter product id (yyyy-MM-dd) (yyyy-MM-dd) (percentage) separated by a space: " +
                    "\nExample: 300 2023-09-10 2023-09-15 75: ");
                string[] userInput = Console.ReadLine().Split(' ');

                if (userInput.Length == 4 &&
                    int.TryParse(userInput[0], out int productId) && (productId >= 100 && productId <= 999) &&
                    DateOnly.TryParseExact(userInput[1], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly startDate) &&
                    DateOnly.TryParseExact(userInput[2], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly endDate) &&
                    decimal.TryParse(userInput[3], out decimal discountPercentage))
                {
                    return (productId, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), discountPercentage);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a 3-digit number, valid dates (yyyy-MM-dd) and a valid percentage value separated by spaces.");
                }
            }
        }
    }
}
