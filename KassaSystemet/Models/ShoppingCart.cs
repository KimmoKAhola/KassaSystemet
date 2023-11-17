using KassaSystemet.Factories.ModelFactory;
using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace KassaSystemet.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            TimeOfPurchase = DateTime.Now;
            Purchases = new();
        }
        public List<Purchase> Purchases { get; set; }
        public DateTime TimeOfPurchase { get; private set; }

        public void AddProductToCart(IUserInputHandler userInputHandler)
        {
            (int id, decimal amount) = userInputHandler.ProductInput();
            if (amount > 100)
                PrintErrorMessage($"You can not purchase more than {100} of a product!");
            else if (ProductCatalogue.Instance.Products.ContainsKey(id))
            {
                Purchases.Add(ModelFactory.CreatePurchase(id, amount));
                Console.ResetColor();
            }
            else
                PrintErrorMessage($"No product with id {id} exist in the system.");
        }

        public void DisplayPurchases()
        {
            if (Purchases.Count == 0)
                PrintErrorMessage("Your shopping cart is empty.");
            else
            {
                PrintSuccessMessage("Your cart contains the following items: ");
                string info = $"{"Product",-20}{"Amount",10}{"Price per kg/per unit",29}\n";
                foreach (var item in Purchases)
                {
                    info += $"{ProductCatalogue.Instance.Products[item.ProductID].ProductName,-20}{item.Amount,10}{ProductCatalogue.Instance.Products[item.ProductID].UnitPrice,20:C2}\n";
                }
                PrintMessage(info);
            }
        }

        public string Pay()
        {
            Console.Clear();
            var receipt = ModelFactory.CreateReceipt(this);
            string result = receipt.CreateReceipt();
            Purchases.Clear();
            PrintSuccessMessage("Your purchase has been made and a receipt has been created.");
            PrintMessage(result);
            return result;
        }

        public decimal CalculateSum(int productId)
        {
            var price = ProductCatalogue.Instance.Products[productId].UnitPrice;
            var sum = 0m;
            foreach (var item in Purchases)
            {
                sum += item.Amount * price;
            }
            if (ProductCatalogue.Instance.Products[productId].HasActiveDiscount())
                sum *= 1 - ProductCatalogue.Instance.Products[productId].Discounts.Max(discount => discount.DiscountPercentage);

            return sum;
        }

        public static decimal GetDiscountPercentage(int productId) => ProductCatalogue.Instance.Products[productId].Discounts.Max(discount => discount.DiscountPercentage);
        public decimal CalculateTotalSum() => Purchases.Sum(product => CalculateSum(product.ProductID));

        private static void PrintSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void PrintMessage(string message) => Console.WriteLine(message);
    }
}