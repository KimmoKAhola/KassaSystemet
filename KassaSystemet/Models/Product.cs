﻿using System.Runtime.CompilerServices;

namespace KassaSystemet.Models
{
    public class Product
    {
        private DateOnly _currentDate = DateOnly.FromDateTime(DateTime.Now);
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Product(string productName, decimal unitPrice, string priceType)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            ProductName = productName;
            if (productName.Length > 20)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Your product name was too long and has been shortened to {ProductName}");
            }
            UnitPrice = unitPrice;
            PriceType = priceType.ToLower();
            _discount = new();
        }
        private List<Discount> _discount;
        private string _productName;
        private int maxProductNameLength = 20;
        public string ProductName
        {
            get => _productName;
            set => _productName = value.Length > maxProductNameLength ? value.Substring(0, maxProductNameLength) : value;
        }
        public decimal UnitPrice { get; set; }
        public string PriceType { get; }
        public List<Discount> Discounts => _discount;
        public static IEnumerable<(int Key, Product Value)> GetDiscountForSingleProduct(Dictionary<int, Product> products)
            => products.Where(p => p.Value.Discounts.Count > 0)
            .Select(pair => (pair.Key, pair.Value));
        public void ChangeProductPrice()
        {
            Console.Write("Enter a new price: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal price) && price > 0)
            {
                UnitPrice = price;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"The price has been changed to {price:C2}.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect input. The price has not been changed.");
            }
        }
        public decimal GetBestDiscount() => Discounts.Max(discount => discount.DiscountPercentage);
        public void ChangeProductName()
        {
            Console.Write("Enter a new product name: ");
            string productName = "";
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            while (productName.Length <= 1)
            {
                Console.Write("Enter a product name, at least 2 character long: ");
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                productName = Console.ReadLine();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            ProductName = productName;
            if (productName.Length > maxProductNameLength)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"Your product name was too long and has been shortened to {productName.Substring(0, maxProductNameLength)}");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Your product name has been set to {ProductName}");
            }
        }
        public void AddDiscountToProduct(Discount d) => _discount.Add(d);
        public void Display() => Discounts.ForEach(x => Console.WriteLine(x.ToString()));
        public bool HasActiveDiscount() => Discounts.Any(discount => _currentDate >= discount.StartDate && _currentDate <= discount.EndDate);
        public override string ToString() => $"Name: {ProductName}, Price: {UnitPrice:C2} {PriceType}";
        public void RemoveDiscount()
        {
            int numberOfDiscounts = Discounts.Count;
            Console.WriteLine("You have the following discounts: ");
            Display();
            Console.Write($"Pick a discount to remove 1-{numberOfDiscounts}: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice < numberOfDiscounts)
            {
                Console.WriteLine($"You removed the discount at {Discounts[choice - 1].StartDate} - {Discounts[choice - 1].EndDate}", Console.ForegroundColor = ConsoleColor.Green);
                Discounts.RemoveAt(choice - 1);
                Console.ResetColor();
                Console.WriteLine("The remaining discounts are:");
                Display();
            }
            else
            {
                Console.WriteLine("Incorrect input.", Console.ForegroundColor = ConsoleColor.Red);
            }
        }
    }
}