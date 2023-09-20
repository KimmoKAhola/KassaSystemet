using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public class Purchase
    {

        /*
         *  Class which handles the "shopping cart".
         *  Currently contains a method to Pay and display current shopping cart
         *  This class is also responsible for calculating the total cost when purchasing
         *  different products
         */

        public Purchase(string productName, int amount)
        {
            ProductName = productName;
            Amount = amount;
        }
        
        public static void Pay()
        {
            //This method creates the receipt for the customer and saves it to the hard drive
            //After the customer has "paid" the cart is cleared
            FileManager.IncrementReceiptCounter();
            FileManager.SaveReceipt(Menu.seedCart, FileManager.GetReceiptID());
            Menu.seedCart.Clear();
        }

        public static void DisplayShoppingCart(List<Purchase> shoppingCart) // Fix this so it can be used in receipt creator
        {
            Console.WriteLine("*******Your current shopping cart contains the following items*******");
            foreach (var item in shoppingCart)
            {
                Console.Write($"\nProduct: {item.ProductName}" +
                    $"  \tamount: {item.Amount}" +
                    $"  \tprice per unit: {Product.FindProductPrice(Menu.seedDictionary, item.ProductName)}" +
                    $"  \tsum: {Product.FindProductPrice(Menu.seedDictionary, item.ProductName) * item.Amount} SEK " +
                    $"  \tproduct id: {Product.GetProductID(Menu.seedDictionary, item.ProductName)}\n");
            }
        }

        public string ProductName { get; set; }
        public int Amount { get; set; }
    }
}
