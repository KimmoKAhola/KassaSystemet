namespace KassaSystemet.Models
{
    public class Purchase
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
        public static string Pay(List<Purchase> shoppingCart)
        {
            if (shoppingCart.Count == 0)
                Console.WriteLine("Your shopping cart is empty. No purchase has been made", Console.ForegroundColor = ConsoleColor.Red);

            Console.Clear();
            string receipt = Receipt.CreateReceipt(shoppingCart);
            shoppingCart.Clear();
            Console.WriteLine("Your purchase has been made and a receipt has been created.", Console.ForegroundColor = ConsoleColor.Green);
            Console.WriteLine(receipt);

            return receipt;
        }
        public static void DisplayPurchases(List<Purchase> shoppingCart)
        {
            if (shoppingCart.Count == 0)
                Console.WriteLine("Your shopping cart is empty.", Console.ForegroundColor = ConsoleColor.Red);
            else
            {
                Console.WriteLine("Your cart contains the following items: ");
                foreach (var item in shoppingCart)
                {
                    string productInfo = $"{ProductCatalogue.Instance.Products[item.ProductID]}, Antal: {item.Amount}";
                    Console.WriteLine(productInfo);
                }
            }
        }
    }
}