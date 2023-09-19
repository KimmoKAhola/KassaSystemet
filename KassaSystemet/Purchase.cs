using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public class Purchase
    {

        //Class which handles the purchase list

        public Purchase(string productName, int amount)
        {
            ProductName = productName;
            Amount = amount;
        }

        public static void Pay()
        {
            // This is the pay method. It should display the shopping cart
            // with its wares, amount and price as well as save the receipt to a file
            // it should then clear the shopping cart for the next customer/purchase
            Menu.receiptID = ++Menu.receiptCounter; // Does not work with 0 and 1 currenty. Fix later
            FileManager.CreateReceiptIDFile(Menu.receiptID);
            FileManager.SaveReceipt(Menu.seedCart, FileManager.GetReceiptID());
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
