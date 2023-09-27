using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public class Discount
    {
        //?This dictionary is ONLY used in the Discount class
        public static Dictionary<int, List<Discount>> allDiscounts = new Dictionary<int, List<Discount>>(); // This contains different discount dates
        //allDiscounts.Add("Bananer", new Discount("2023/09/20", "2023/09/25", 0.5m));
        public Discount(string startDate, string endDate, decimal discountPercentage) // Write discountPercentage as eg 70 %
        {
            //EndDate = endDate; DateTime.Parse(“07/10/2022”);
            StartDate = DateTime.Parse(startDate, CultureInfo.CurrentCulture); //!https://www.codingninjas.com/studio/library/how-to-convert-string-to-datetime-in-csharp
            EndDate = DateTime.Parse(endDate, CultureInfo.CurrentCulture); //!!Useful link above to improve on this class later
            //TODO add error handling. Discount percentage has to be between 0-100 %.
            DiscountPercentage = discountPercentage / 100m;
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProductID { get; set; }
        public decimal DiscountPercentage { get; set; }
        public static DateTime CurrentDate()
        {
            return DateTime.Now;
        }
        public static decimal GetCurrentDiscountPercentage(int productID)
        {
            if (IsProductOnSale(productID))
            {
                //If there is a discount on this given date we need to find it
                DateTime currentDate = CurrentDate();
                var availableDiscounts = allDiscounts[productID];
                foreach (var discount in availableDiscounts)
                {
                    if (discount.StartDate <= currentDate && currentDate <= discount.EndDate)
                    {
                        return discount.DiscountPercentage; // Maybe right??
                    }
                }
            }
            return -100m; //TODO Error handling
        }
        public static void RemoveDiscount(Dictionary<int, List<Discount>> allDicsounts, int productID, string startDate, string endDate)
        {
            if (allDicsounts.ContainsKey(productID))
            {
                var currentProduct = allDicsounts[productID];
                foreach (var item in currentProduct)
                {
                    string temp = DateTime.Parse(startDate, CultureInfo.CurrentCulture).ToShortDateString();
                    string temp2 = DateTime.Parse(endDate, CultureInfo.CurrentCulture).ToShortDateString();
                    if (temp == startDate || temp2 == endDate)
                    {
                        Console.WriteLine($"Removed the discount for product id [{productID}] with discount dates [{startDate}]-[{endDate}]");
                        allDicsounts[productID].Remove(item);
                    }
                    break;
                }
            }
        }
        public static bool IsDiscountListEmpty()
        {
            if (allDiscounts.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool IsProductOnSale(int productID) // Send out true if currentDate is between startDate and endDate
        {
            if (!allDiscounts.ContainsKey(productID))
            {
                return false; //If the dictionary does not contain the key, it is not on sale
            }
            DateTime currentDate = CurrentDate();
            foreach (var discount in allDiscounts[productID])
            {
                if (discount.StartDate <= currentDate && currentDate <= discount.EndDate)
                {
                    return true; // The product key exists. Now check if the date is correct.
                }
            }
            return false;
        }
        public static void AddNewDiscount(int productID, string startDate, string endDate, decimal discountPercentage)
        {
            DateTime start = DateTime.Parse(startDate, CultureInfo.CurrentCulture);
            DateTime end = DateTime.Parse(endDate, CultureInfo.CurrentCulture);
            if (start.CompareTo(end) < 0) //Checks that start date is earlier than end date
            {
                Discount newDiscount = new(startDate, endDate, discountPercentage);
                if (!allDiscounts.ContainsKey(productID) && (discountPercentage > 0 && discountPercentage < 100))
                {
                    List<Discount> discountsPerItem = new();
                    discountsPerItem.Add(newDiscount); // Key is product name
                    allDiscounts.Add(productID, discountsPerItem);
                }
                else if ((discountPercentage <= 0 || discountPercentage >= 100))
                {
                    Console.WriteLine($"Your discount percentage of {discountPercentage} is not valid. Enter a value between 0 and 100.");
                }
                else
                {
                    allDiscounts[productID].Add(newDiscount);//if productname already exists, then do not create a new addition to the discountDictionary
                }
            }
            else
            {
                Console.WriteLine("The start date can not be after the end date. The discount has not been added.");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }
        public static void PrintDiscount(Dictionary<int, Discount> dictionary) // This prints out products which have had discounts with their dates.
        {
            //TODO fix this so that it prints all available discounts per product (KEY) in discountDictionary.
            Console.WriteLine("\nProduct ID(KEY)\tDiscount price\tDiscount is valid between");
            foreach (var item in dictionary)
            {
                Console.Write($"{item.Key}\t\t\t{item.Value.DiscountPercentage}\t\t[{item.Value.StartDate.ToShortDateString()}]-[{item.Value.EndDate.ToShortDateString()}]\n");
            }
        }
        public static string CreateDiscountString(Dictionary<int, List<Discount>> allDiscounts)
        {
            if (IsDiscountListEmpty())
            {
                return "";
            }
            else
            {
                allDiscounts = allDiscounts.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                string formattedDiscountListString = "";
                foreach (var item in allDiscounts)
                {
                    formattedDiscountListString += item.Key + "!";
                    foreach (var discount in item.Value)
                    {
                        formattedDiscountListString += discount.StartDate.ToShortDateString() + "!" + discount.EndDate.ToShortDateString() + "!" + discount.DiscountPercentage * 100m + "!";
                    }
                    formattedDiscountListString += "\n";
                }
                return formattedDiscountListString.Remove(formattedDiscountListString.Length - 1) + "\n";
            }
        }
        public static void DisplayAllDiscounts(Dictionary<int, List<Discount>> allDiscounts)
        {
            Console.WriteLine("****A list of all discounts in the system****\n");
            foreach (var item in allDiscounts)
            {
                Console.Write($"The product with ID: [{item.Key}]");
                Console.Write($" has discount start and end dates: \n");
                foreach (var discount in item.Value)
                {
                    Console.Write($"[{discount.StartDate.ToShortDateString()}]-[{discount.EndDate.ToShortDateString()}] and discount percentage {discount.DiscountPercentage * 100:F2} %\n");
                }
            }
            Console.WriteLine();
        }
    }
}
