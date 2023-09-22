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
        public static Dictionary<string, List<Discount>> allDiscounts = new Dictionary<string, List<Discount>>(); // This contains different discount dates
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
        public string ProductName { get; set; }
        public decimal DiscountPercentage { get; set; }
        public static DateTime CurrentDate()
        {
            return DateTime.Now;
        }

        public static decimal GetCurrentDiscountPercentage(string productName)
        {
            if (IsProductOnSale(productName))
            {
                //If there is a discount on this given date we need to find it
                DateTime currentDate = CurrentDate();
                var availableDiscounts = allDiscounts[productName];
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
        public static void RemoveDiscount()
        {
            Console.WriteLine("Not implemented yet!");
        }

        public static bool IsProductOnSale(string productName) // Send out true if currentDate is between startDate and endDate
        {
            if (!allDiscounts.ContainsKey(productName))
            {
                return false; //If the dictionary does not contain the key, it is not on sale
            }
            DateTime currentDate = CurrentDate();
            foreach (var discount in allDiscounts[productName])
            {
                if (discount.StartDate <= currentDate && currentDate <= discount.EndDate)
                {
                    return true; // The product key exists. Now check if the date is correct.
                }
            }
            return false;
        }
        public static void AddNewDiscount(string productName, string startDate, string endDate, decimal discountPercentage)
        {
            //TODO Needs error handling to check if startDate and endDate already exists.
            Discount newDiscount = new Discount(startDate, endDate, discountPercentage);
            if (!allDiscounts.ContainsKey(productName))
            {
                List<Discount> discountsPerItem = new List<Discount>();
                discountsPerItem.Add(newDiscount); // Key is product name
                allDiscounts.Add(productName, discountsPerItem);
            }
            else
            {
                allDiscounts[productName].Add(newDiscount);//if productname already exists, then do not create a new addition to the discountDictionary
            }
        }
        public static void PrintDiscount(Dictionary<string, Discount> dictionary) // This prints out products which have had discounts with their dates.
        {
            //TODO fix this so that it prints all available discounts per product (KEY) in discountDictionary.
            Console.WriteLine("\nProduct Name(KEY)\tDiscount price\tDiscount is valid between");
            foreach (var item in dictionary)
            {
                Console.Write($"{item.Key}\t\t\t{item.Value.DiscountPercentage}\t\t[{item.Value.StartDate.ToShortDateString()}]-[{item.Value.EndDate.ToShortDateString()}]\n");
            }
        }
        public static string CreateDiscountString(Dictionary<string, List<Discount>> allDiscounts)
        {
            string formattedDiscountListString = "";
            foreach (var item in allDiscounts)
            {
                formattedDiscountListString += item.Key + "!";
                foreach (var discount in item.Value)
                {
                    formattedDiscountListString += discount.StartDate.ToShortDateString() + "!" + discount.EndDate.ToShortDateString() + "!" + discount.DiscountPercentage*100m + "!";
                }
                formattedDiscountListString += "\n";
            }
            return formattedDiscountListString.Remove(formattedDiscountListString.Length - 1) + "\n";
        }
        public static void DisplayAllDiscounts(Dictionary<string, List<Discount>> allDiscounts)
        {
            Console.WriteLine("****A list of all discounts in the system****\n");
            foreach (var item in allDiscounts)
            {
                Console.Write($"The product with name: [{item.Key}]");
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
