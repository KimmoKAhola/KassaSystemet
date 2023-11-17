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
                PrintSpecialMessage($"Your product is sold per unit and the amount has been rounded to {amount}");
            }
            Amount = amount;
            PrintSuccessMessage($"Added the product {ProductCatalogue.Instance.Products[productID].ProductName} with ID [{ProductID}] and amount {Amount} to your cart.");
        }
        public int ProductID { get; }
        public decimal Amount { get; }

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