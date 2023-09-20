using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet
{
    public class Purchase
    {
        //TODO This is only used for seeding purposes
        //public static List<Purchase> shoppingCart = new(){
        //    { new Purchase("Bananer", 10)},
        //    { new Purchase("Äpplen", 7) },
        //    { new Purchase("Choklad", 1) },
        //    { new Purchase("Pepsi", 3) },
        //    { new Purchase("Kexchok", 2) },
        //    { new Purchase("Sallad", 1) },
        //    { new Purchase("Jordgub", 3) },
        //    { new Purchase("Nutella", 2) },
        //    { new Purchase("Toapapp", 10) },
        //    { new Purchase("Saffran", 25) },
        //    { new Purchase("Vatten", 3) } };
        public static List<Purchase> shoppingCart = new(){
            { new Purchase("Bananer", 10)} };

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
            FileManager.SaveReceipt(shoppingCart, FileManager.GetReceiptID());
            //Receipt.Test(shoppingCart, Discount.allDiscounts); //TODO remove this
            shoppingCart.Clear();
        }

        public static void DisplayShoppingCart(List<Purchase> shoppingCart) // Fix this so it can be used in receipt creator
        {
            Console.WriteLine("*******Your current shopping cart contains the following items*******");
            foreach (var item in shoppingCart)
            {
                Console.Write($"\nProduct: {item.ProductName}" +
                    $"  \tamount: {item.Amount}" +
                    $"  \tprice {Product.FindProductPriceType(Product.productDictionary, item.ProductName)}: {Product.FindProductPrice(Product.productDictionary, item.ProductName)} SEK" +
                    $"  \ttotal sum: {Product.FindProductPrice(Product.productDictionary, item.ProductName) * item.Amount} SEK " +
                    $"  \tproduct id: {Product.GetProductID(Product.productDictionary, item.ProductName)}\n");
            }
        }

        public string ProductName { get; set; }
        public int Amount { get; set; }
    }
}
