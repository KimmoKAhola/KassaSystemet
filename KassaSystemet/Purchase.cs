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
        public Purchase(int productID, decimal amount)
        {
            ProductID = productID;
            if (Product.CheckPriceType(productID))
            {
                Amount = decimal.Round(amount, 2);
            }
            else
            {
                Console.WriteLine($"Your product with id [{productID}] is not sold per kg and your requested amount of {amount}" +
                    $" has been rounded to {Math.Round(amount)}");
                Amount = Math.Round(amount);
            }
            Console.WriteLine($"Added product ID [{productID}] and amount {amount} to your cart.");
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
                        $"  \tprice {currentProductType}: {currentPrice:C2}" +
                        $"  \ttotal sum: {(currentPrice * item.Amount):C2} " +
                        $"  \tproduct id: {item.ProductID}\n");
                }
            }
        }

        /// <summary>
        /// This method returns the longest string length
        /// for all purchases in the shopping cart.
        /// This is then returned and used to format
        /// the receipt when using the Pay method.
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        public static int GetLongestName(List<Purchase> shoppingCart)
        {
            int maxLength = 0;
            foreach (var item in shoppingCart)
            {
                if(Product.GetProductName(Product.productDictionary, item.ProductID).Length > maxLength)
                {
                    maxLength = Product.GetProductName(Product.productDictionary, item.ProductID).Length;
                }
            }
            return maxLength;
        }
        public int ProductID { get; }
        public decimal Amount { get; }
    }
}
