using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            DateTime timeOfPurchase = DateTime.Now;
            Purchases = new();
        }
        public List<Purchase> Purchases { get; set; }

        public void AddProductToCart(IUserInputHandler userInputHandler)
        {
            (int id, decimal amount) = userInputHandler.ProductInput();
            if (amount > 100)
                Console.WriteLine($"You can not purchase more than {100} of a product!", ConsoleColor.Red);
            else if (ProductCatalogue.Instance.Products.ContainsKey(id))
                Purchases.Add(ModelFactory.CreatePurchase(id, amount));
            else
                Console.WriteLine($"No product with id {id} exist in the system.", ConsoleColor.Red);
        }

        public void DisplayPurchases()
        {
            if (Purchases.Count == 0)
                Console.WriteLine("Your shopping cart is empty.", Console.ForegroundColor = ConsoleColor.Red);
            else
            {
                Console.WriteLine("Your cart contains the following items: ");
                foreach (var item in Purchases)
                {
                    string productInfo = $"{ProductCatalogue.Instance.Products[item.ProductID]}, Antal: {item.Amount}";
                    Console.WriteLine(productInfo);
                }
            }
        }

        public string Pay()
        {
            if (Purchases.Count == 0)
                Console.WriteLine("Your shopping cart is empty. No purchase has been made", Console.ForegroundColor = ConsoleColor.Red);

            foreach (var item in Purchases)
            {

            }

            Console.Clear();
            string receipt = Receipt.CreateReceipt(this);
            Purchases.Clear();
            Console.WriteLine("Your purchase has been made and a receipt has been created.", Console.ForegroundColor = ConsoleColor.Green);
            Console.WriteLine(receipt);
            return receipt;
        }

        public decimal CalculateSum(int productId)
        {
            var products = ProductCatalogue.Instance.Products;
            var sum = products[productId].UnitPrice;

            if (products[productId].HasActiveDiscount())
                sum *= 1 - products[productId].Discounts.Max(discount => discount.DiscountPercentage);

            return sum;
        }

        public decimal CalculateTotalSum()
        {
            var products = ProductCatalogue.Instance.Products;
            var sum = 0m;
            foreach (var item in Purchases)
            {
                sum += CalculateSum(item.ProductID);
            }
            return sum;
        }
    }
}
