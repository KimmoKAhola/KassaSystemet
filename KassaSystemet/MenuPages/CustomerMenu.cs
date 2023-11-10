using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.MenuPages
{
    public class CustomerMenu : IMenu
    {
        public CustomerMenu()
        {

        }

        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("****Welcome to the customer menu****");
            Console.WriteLine("1. Display your current cart.");
            Console.WriteLine("2. Enter products to purchase.");
            Console.WriteLine("3. Display available products.");
            Console.WriteLine("0: Clear shoppingcart and return to main menu.");
            Console.WriteLine("PAY: purchase wares in your cart and exit.");
            Console.Write("Enter command: ");
            Console.ReadKey();
        }
    }
}

//private static void CustomerMenu(FileManager fileManager)
//{
//    List<Purchase> shoppingCart = new();
//    string userInput;
//    do
//    {
//        Console.Clear();
//        Console.WriteLine("****Welcome to the customer menu****");
//        Console.WriteLine("1. Display your current cart.");
//        Console.WriteLine("2. Enter products to purchase.");
//        Console.WriteLine("3. Display available products.");
//        Console.WriteLine("0: Clear shoppingcart and return to main menu.");
//        Console.WriteLine("PAY: purchase wares in your cart and exit.");
//        Console.Write("Enter command: ");
//        userInput = Console.ReadLine().ToUpper();
//        switch (userInput)
//        {
//            case "1":
//                Purchase.DisplayPurchases(shoppingCart);
//                break;
//            case "2":
//                AddProduct(shoppingCart);
//                break;
//            case "3":
//                ProductCatalogue.Instance.DisplayProducts();
//                break;
//            case "0":
//                userInput = "0";
//                Console.WriteLine("Returning to the main menu.");
//                break;
//            case "PAY":
//                string receipt = Purchase.Pay(shoppingCart);
//                fileManager.SaveReceipt(receipt);
//                break;
//            default:
//                Console.WriteLine("Invalid input.", Console.ForegroundColor = ConsoleColor.Red);
//                Thread.Sleep(1000);
//                Console.ResetColor();
//                break;
//        }
//        Console.ResetColor();
//        Console.Write("Press any key to continue: ");
//        Console.ReadKey();
//    } while (userInput != "0");
//    MainMenu(fileManager);
//}
