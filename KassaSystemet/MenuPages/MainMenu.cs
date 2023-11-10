using KassaSystemet;
using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.MenuPageServices;
using KassaSystemet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.MenuPages
{
    public class MainMenu
    {
        private static AdminMenuHandler adminMenuHandler;
        private MenuFactory menuFactory;
        public void Start(FileManager fileManager)
        {
            do
            {
                string menuOption = Console.ReadLine();
                {
                    switch (menuOption)
                    {
                        case "1":
                            //menuFactory = new MenuFactory("Customer Menu");
                            //CustomerMenu(fileManager);
                            break;
                        case "2":
                            //menuFactory = new("Admin Menu");
                            //AdminMenu(fileManager);
                            break;
                        case "0":
                            App.CloseApp();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid input.");
                            Thread.Sleep(1000);
                            Console.ResetColor();
                            break;
                    }
                }
            } while (true);
        }
    }
}

//private static void AddProduct(List<Purchase> shoppingCart)
//{
//    (int id, decimal amount) = UserInputHandler.ProductInput();
//    if (amount > 100)
//        Console.WriteLine($"You can not purchase more than {100} of a product!", ConsoleColor.Red);
//    else if (ProductCatalogue.Instance.Products.ContainsKey(id))
//        shoppingCart.Add(new Purchase(id, amount));
//    else
//        Console.WriteLine($"No product with id {id} exist in the system.", ConsoleColor.Red);
//}
