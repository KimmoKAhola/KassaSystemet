using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public class Discount
    {
        public Discount(DateTime startDate, DateTime endDate, string productName, decimal discountPrice)
        {
            StartDate = startDate; // ex new DateTime(2023, 9, 1)
            EndDate = endDate;
            ProductName = productName;
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
        public static void AddNewDiscount(Dictionary<string, Discount> discountDictionary, string productName, decimal discountPrice, DateTime startDate, DateTime endDate)
        {

            Seed.discountDictionary.Add(productName, new Discount(startDate, endDate, productName, discountPrice));
        }
    }
}
