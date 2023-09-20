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
        public static Dictionary<string, Discount> discountDictionary = new() { }; // Key should be product name.
        public Discount(string startDate, string endDate, decimal discountPrice)
        {
            //StartDate = startDate; // ex new DateTime(2023, 9, 1)
            //EndDate = endDate; DateTime.Parse(“07/10/2022”);
            StartDate = DateTime.Parse(startDate, CultureInfo.CurrentCulture); //!https://www.codingninjas.com/studio/library/how-to-convert-string-to-datetime-in-csharp
            EndDate = DateTime.Parse(endDate); //!!Useful link above to improve on this class later
            DiscountPrice = discountPrice; // ex 0.20m for 20 % off
        } // Should add start date, end date and discount price ("KAMPANJ")
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProductName { get; set; }
        public decimal DiscountPrice { get; set; }
        public DateTime CurrentDate()
        {
            return DateTime.Now;
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
            discountDictionary.Add(key, discount); // Key is product name
        }
        public static void PrintDiscount(Dictionary<string, Discount> dictionary)
        {
            Console.WriteLine("\nProduct Name(KEY)\tDiscount price\tDiscount valid is between");
            foreach (var item in discountDictionary)
            {
                Console.Write($"{item.Key}\t\t\t{item.Value.DiscountPrice}\t\t[{item.Value.StartDate.ToShortDateString()}]-[{item.Value.EndDate.ToShortDateString()}]\n");
            }
        }
    }
}
