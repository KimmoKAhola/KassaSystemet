using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public class Purchase
    {
        /// <summary>
        /// Constructor for my shopping cart. Fills a with purchases based on product ID
        /// and amount. This is then connected to my product Dictionary when making a
        /// purchase using Pay().
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="amount"></param>
        public Purchase(int productID, decimal amount)
        {
            ProductID = productID;
            Amount = amount;
            Console.WriteLine($"Added product ID [{ProductID}] and amount {Amount} to your cart.");
        }
        public int ProductID { get; }
        public decimal Amount { get; }

        /// <summary>
        /// This method performs the payment.
        /// Saves the receipt and clears the shopping cart afterwards.
        /// </summary>
        public static void Pay(List<Purchase> shoppingCart, Dictionary<int, Product> products)
        {
            //This method creates the receipt for the customer and saves it to the hard drive
            //After the customer has "paid" the cart is cleared
            if (shoppingCart.Count == 0)
            {
                Console.WriteLine("Your shopping cart is empty. No purchase has been made");
            }
            else
            {
                string receipt = Receipt.CreateReceipt(shoppingCart, products);
                FileManager.SaveReceipt(receipt);
                shoppingCart.Clear();
            }
        }
        public static void DisplayPurchases(List<Purchase> shoppingCart, Dictionary<int, Product> products)
        {
            if (shoppingCart.Count == 0)
            {
                Console.WriteLine("Your shopping cart is empty.");
            }
            else
            {
                Console.WriteLine("Varukorgen består av: ");
                foreach (var item in shoppingCart)
                {
                    string productInfo = $"{products[item.ProductID]}, Antal: {item.Amount}";
                    Console.WriteLine(productInfo);
                }
            }
        }
    }
}
