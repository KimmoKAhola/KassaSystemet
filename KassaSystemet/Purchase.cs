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
            { new Purchase(300, 10)} };

        /*
         *  Class which handles the "shopping cart".
         *  Currently contains a method to Pay and display current shopping cart
         *  This class is also responsible for calculating the total cost when purchasing
         *  different products
         */

        /// <summary>
        /// Constructor for my shopping cart. Fills a with purchases based on product ID
        /// and amount. This is then connected to my product Dictionary when making a
        /// purchase using Pay().
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="amount"></param>
        public Purchase(int productID, int amount)
        {
            ProductID = productID;
            Amount = amount;
        }

        /// <summary>
        /// This method performs the payment.
        /// Saves the receipt and clears the shopping cart afterwards.
        /// </summary>
        public static void Pay()
        {
            //This method creates the receipt for the customer and saves it to the hard drive
            //After the customer has "paid" the cart is cleared
            if (shoppingCart.Count == 0)
            {
                Console.WriteLine("Your shopping cart is empty. No purchase has been made");
            }
            else
            {
                FileManager.SaveReceipt(shoppingCart, FileManager.GetReceiptID());
                shoppingCart.Clear();
            }
        }

        public static void DisplayShoppingCart(List<Purchase> shoppingCart) // Fix this so it can be used in receipt creator
        {
            if (shoppingCart.Count == 0)
            {
                Console.WriteLine("Your shopping cart is currently empty.");
            }
            else
            {
                Console.WriteLine("*******Your current shopping cart contains the following items*******");
                foreach (var item in shoppingCart)
                {
                    string currentProductName = Product.GetProductName(Product.productDictionary, item.ProductID);
                    string currentProductType = Product.FindProductPriceType(Product.productDictionary, item.ProductID);
                    decimal currentPrice = Product.FindProductPrice(Product.productDictionary, item.ProductID);

                    Console.Write($"\nProduct: {currentProductName}" +
                        $"  \tamount: {item.Amount}" +
                        $"  \tprice {currentProductType}: {currentPrice} SEK" +
                        $"  \ttotal sum: {currentPrice * item.Amount} SEK " +
                        $"  \tproduct id: {item.ProductID}\n");
                }
            }
        }
        public int ProductID { get; }
        public int Amount { get; }
    }
}
