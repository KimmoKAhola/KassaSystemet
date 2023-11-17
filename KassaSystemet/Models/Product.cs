using KassaSystemet.Interfaces;
using System.Runtime.CompilerServices;

namespace KassaSystemet.Models
{
    public class Product : IProduct
    {
        private DateOnly _currentDate = DateOnly.FromDateTime(DateTime.Now);
        public Product(string productName, decimal unitPrice, string priceType)
        {
            ProductName = productName;
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
            set
            {
                if (value.Length > maxProductNameLength)
                {
                    _productName = value.Substring(0, maxProductNameLength);
                    PrintSpecialMessage($"Your product name was too long and has been shortened to {ProductName}");
                }
                else
                    _productName = value;
            }
        }
        public decimal UnitPrice { get; set; }
        public string PriceType { get; }
        public List<Discount> Discounts => _discount;
        public static IEnumerable<(int Key, Product Value)> GetDiscountForSingleProduct(Dictionary<int, Product> products)
            => products.Where(p => p.Value.Discounts.Count > 0)
            .Select(pair => (pair.Key, pair.Value));
        public decimal GetBestDiscount() => Discounts.Max(discount => discount.DiscountPercentage);
        public void AddDiscountToProduct(Discount d) => _discount.Add(d);
        public void Display() => Discounts.ForEach(x => Console.WriteLine(x.ToString()));
        public bool HasActiveDiscount() => Discounts.Any(discount => _currentDate >= discount.StartDate && _currentDate <= discount.EndDate);
        public override string ToString() => $"Name: {ProductName}, Price: {UnitPrice:C2} {PriceType}";
        public void RemoveDiscount()
        {
            int numberOfDiscounts = Discounts.Count;
            PrintSuccessMessage("You have the following discounts: ");
            Display();
            Console.Write($"Pick a discount to remove. Choose discount between: 1-{numberOfDiscounts}: ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= numberOfDiscounts)
            {
                PrintSuccessMessage($"You removed the discount at [{Discounts[choice - 1].StartDate}] - [{Discounts[choice - 1].EndDate}] with a discount [{Discounts[choice - 1].DiscountPercentage:P2}]");
                Discounts.RemoveAt(choice - 1);
                PrintSuccessMessage("The remaining discounts are:");
                Display();
            }
            else
                PrintErrorMessage("Invalid input.");
        }
        private static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void PrintSpecialMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
        private static void PrintSuccessMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}