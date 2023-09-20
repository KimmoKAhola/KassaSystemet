using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public class Discount
    {
        public static Dictionary<string, Discount> discountDictionary = new() { }; // Key should be product name.
        public Discount(DateTime startDate, DateTime endDate, decimal discountPrice)
        {
            StartDate = startDate; // ex new DateTime(2023, 9, 1)
            EndDate = endDate;
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
            Console.WriteLine("\nProduct Name(KEY)\tDiscount price\t");
            foreach (var item in discountDictionary)
            {
                Console.Write($"{item.Key}\t\t\t{item.Value.DiscountPrice}\t\n");
            }
        }
    }
}
