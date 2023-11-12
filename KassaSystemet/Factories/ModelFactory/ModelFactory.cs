using KassaSystemet.Interfaces;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Factories.ModelFactory
{
    public static class ModelFactory
    {
        public static Product CreateProduct(string productName, decimal unitPrice, string priceType)
        {
            return new Product(productName, unitPrice, priceType);
        }

        public static Purchase CreatePurchase(int productID, decimal amount)
        {
            return new Purchase(productID, amount);
        }

        public static Discount CreateDiscount(string startDate, string endDate, decimal discountPercentage)
        {
            return new Discount(startDate, endDate, discountPercentage);
        }

        public static ShoppingCart CreateShoppingCart()
        {
            return new ShoppingCart();
        }

        public static Receipt CreateReceipt(ShoppingCart shoppingCart)
        {
            return new Receipt(shoppingCart);
        }
    }
}
