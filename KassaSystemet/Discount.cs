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
        //public static Dictionary<string, Discount> discountDictionary = new(); //TODO is this needed?
        //TODO This is only used for seeding purposes
        //public static Dictionary<string, Discount> discountDictionary = new()
        //{
        //    //{ }
        //    //{"Bananer", new Discount("2023/09/20", "2023/09/25", 5.00m)},
        //    //{"Äpplen", new Discount("2023/09/20", "2023/09/22", 12.0m) }
        //}; // Key should be product name.
        public Discount(string startDate, string endDate, decimal discountPercentage) // Write discountPercentage as eg 70 %
        {
            //EndDate = endDate; DateTime.Parse(“07/10/2022”);
            StartDate = DateTime.Parse(startDate, CultureInfo.CurrentCulture); //!https://www.codingninjas.com/studio/library/how-to-convert-string-to-datetime-in-csharp
            EndDate = DateTime.Parse(endDate, CultureInfo.CurrentCulture); //!!Useful link above to improve on this class later
            //TODO add error handling. Discount percentage has to be between 0-100 %.
            DiscountPrice = discountPercentage/100m;
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ProductName { get; set; }
        public decimal DiscountPrice { get; set; }
        public static DateTime CurrentDate()
        {
            return DateTime.Now;
        }

        public decimal GetDiscountPercentage()
        {
            //Is this needed?
            //return DiscountPrice/100m;
            return 0m;
        }
        public static bool IsProductOnSale(string productName) // Send out true if currentDate is between startDate and endDate
        {
            if (allDiscounts.ContainsKey(productName))
            {
                DateTime date = allDiscounts[productName].EndDate;

                if (DateTime.Compare(date, CurrentDate()) == 1)
                {
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        public static void AddNewDiscount(string productName, string startDate, string endDate, decimal discountPercentage)
        {
            //TODO Needs error handling to check if the key already exists or not.
            Discount newDiscount = new Discount(startDate, endDate, discountPercentage/100m);
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
                Console.Write($"{item.Key}\t\t\t{item.Value.DiscountPrice}\t\t[{item.Value.StartDate.ToShortDateString()}]-[{item.Value.EndDate.ToShortDateString()}]\n");
            }
        }
    }
}
