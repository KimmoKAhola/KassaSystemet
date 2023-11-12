using KassaSystemet.Interfaces;

namespace KassaSystemet.Models
{
    public class Purchase : IPurchase
    {
        public Purchase(int productID, decimal amount)
        {
            ProductID = productID;
            if (ProductCatalogue.Instance.Products[productID].PriceType.ToLower() == "per unit" && amount % 1 != 0)
            {
                amount = amount < 1 ? 1 : (int)amount;
                Console.WriteLine($"Your product is sold per unit and the amount has been rounded down to {amount}", Console.ForegroundColor = ConsoleColor.DarkYellow);
            }
            Amount = amount;
            Console.WriteLine($"Added the product {ProductCatalogue.Instance.Products[productID].ProductName} with ID [{ProductID}] and amount {Amount} to your cart.", Console.ForegroundColor = ConsoleColor.Green);
        }
        public int ProductID { get; }
        public decimal Amount { get; }
    }
}