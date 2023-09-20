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
        public Discount(string startDate, string endDate, decimal discountPrice)
        {
            //EndDate = endDate; DateTime.Parse(“07/10/2022”);
            StartDate = DateTime.Parse(startDate, CultureInfo.CurrentCulture); //!https://www.codingninjas.com/studio/library/how-to-convert-string-to-datetime-in-csharp
            EndDate = DateTime.Parse(endDate); //!!Useful link above to improve on this class later
            DiscountPrice = discountPrice; // ex 0.20m for 20 % off
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProductName { get; set; }
        public decimal DiscountPrice { get; set; }
        public DateTime CurrentDate()
        {
            return DateTime.Now;
        }

        public decimal GetDiscountPrice()
        {
            //Is this needed?
            return 0m;
        }
        public bool IsProductOnSale() // Send out true if currentDate is between startDate and endDate
        {
            if (DateTime.Compare(EndDate, CurrentDate())==1)
            {
                return true;
            }
            else
                return false;
        }
        public static void AddNewDiscount(string key, Discount discount)
        {
            //TODO Needs error handling to check if the key already exists or not.
            Seed.discountDictionary.Add(key, discount); // Key is product name
        }
        public static void PrintDiscount(Dictionary<string, Discount> dictionary) // This prints out products which have had discounts with their dates.
        {
            Console.WriteLine("\nProduct Name(KEY)\tDiscount price\tDiscount is valid between");
            foreach (var item in Seed.discountDictionary)
            {
                Console.Write($"{item.Key}\t\t\t{item.Value.DiscountPrice}\t\t[{item.Value.StartDate.ToShortDateString()}]-[{item.Value.EndDate.ToShortDateString()}]\n");
            }
        }
    }
}
