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
        public Discount(string startDate, string endDate, decimal discountPercentage)
        {
            if (startDate.CompareTo(endDate) < 0 && discountPercentage < 100 && discountPercentage > 0)
            {
                StartDate = DateOnly.Parse(startDate);
                EndDate = DateOnly.Parse(endDate);
                DiscountPercentage = discountPercentage / 100m;
            }
        }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal DiscountPercentage { get; set; }
        public override string ToString() => $"Discount is valid between [{StartDate}] - [{EndDate}] with a discount of [{DiscountPercentage * 100m:F2} %]";
    }
}