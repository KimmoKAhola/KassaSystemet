using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Factories.ModelFactory
{
    public class ModelFactory
    {
        public Product CreateProduct(string productName, decimal unitPrice, string priceType)
        {
            return new Product(productName, unitPrice, priceType);
        }

        public Purchase CreatePurchase(int productID, decimal amount)
        {
            return new Purchase(productID, amount);
        }

        public Discount CreateDiscount(string startDate, string endDate, decimal discountPercentage)
        {
            return new Discount(startDate, endDate, discountPercentage);
        }
    }
}
