
namespace KassaSystemet
{
    public class Purchase
    {
        public Purchase(int productID, decimal amount)
        {
            ProductID = productID;
            Amount = amount;
            Console.WriteLine($"Added product ID [{ProductID}] and amount {Amount} to your cart.");
        }
        public int ProductID { get; }
        public decimal Amount { get; }
        public static void Pay(List<Purchase> shoppingCart, Dictionary<int, Product> products)
        {
            if (shoppingCart.Count == 0)
                Console.WriteLine("Your shopping cart is empty. No purchase has been made");
            else
            {
                string receipt = Receipt.CreateReceipt(shoppingCart, products);
                FileManager.SaveReceipt(receipt);
                shoppingCart.Clear();
                Console.WriteLine("Your purchase has been made and a receipt has been created.");
            }
        }
        public static void DisplayPurchases(List<Purchase> shoppingCart, Dictionary<int, Product> products)
        {
            if (shoppingCart.Count == 0)
                Console.WriteLine("Your shopping cart is empty.");
            else
            {
                Console.WriteLine("Your cart contains the following items: ");
                foreach (var item in shoppingCart)
                {
                    string productInfo = $"{products[item.ProductID]}, Antal: {item.Amount}";
                    Console.WriteLine(productInfo);
                }
            }
        }
    }
}