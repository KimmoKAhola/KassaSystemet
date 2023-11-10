using KassaSystemet.Factories.MenuFactory;
using KassaSystemet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.MenuPages
{
    public class CustomerMenu : IMenuDisplay
    {
        public CustomerMenu()
        {

        }
        public void DisplayMenu()
        {
            do
            {
                string menuOption = Console.ReadLine();
                {
                    switch (menuOption)
                    {
                        case "1":
                            menuFactory = new MenuFactory("Customer Menu");
                            CustomerMenu(fileManager);
                            break;
                        case "2":
                            menuFactory = new MenuFactory("Admin Menu");
                            AdminMenu(fileManager);
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
