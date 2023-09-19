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

            Receipt.CreateReceiptForCart(Menu.testCart, Receipt.GetReceiptID());
        }

        public static void DisplayShoppingCart(List<Purchase> shoppingCart)
        {
            Console.WriteLine("Your cart has the items: ");
            foreach (var item in shoppingCart)
            {
                Console.Write($"Product{item.ProductName}, amount: {item.Amount}\n");
            }
        }

        public string ProductName { get; set; }
        public int Amount { get; set; }
    }
}
