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
                Purchases.Add(ModelFactory.CreatePurchase(id, amount));
            else
                PrintErrorMessage($"No product with id {id} exist in the system.");
        }

        public void DisplayPurchases()
        {
            if (Purchases.Count == 0)
                PrintErrorMessage("Your shopping cart is empty.");
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
            Console.Clear();
            var receipt = ModelFactory.CreateReceipt(this);
            string result = receipt.CreateReceipt();
            Purchases.Clear();
            PrintMessage("Your purchase has been made and a receipt has been created.");
            PrintMessage(result);
            return result;
        }

        public decimal CalculateSum(int productId)
        {
            var sum = ProductCatalogue.Instance.Products[productId].UnitPrice;

            if (ProductCatalogue.Instance.Products[productId].HasActiveDiscount())
                sum *= 1 - ProductCatalogue.Instance.Products[productId].Discounts.Max(discount => discount.DiscountPercentage);

            return sum;
        }

        public decimal CalculateTotalSum() => Purchases.Sum(product => CalculateSum(product.ProductID));

        private void PrintMessage(string message) => Console.WriteLine(message);
        private void PrintErrorMessage(string message) => Console.WriteLine(message);
    }
}
